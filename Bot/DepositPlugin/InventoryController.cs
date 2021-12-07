//     _                _      _  ____   _                           _____
//    / \    _ __  ___ | |__  (_)/ ___| | |_  ___   __ _  _ __ ___  |  ___|__ _  _ __  _ __ ___
//   / _ \  | '__|/ __|| '_ \ | |\___ \ | __|/ _ \ / _` || '_ ` _ \ | |_  / _` || '__|| '_ ` _ \
//  / ___ \ | |  | (__ | | | || | ___) || |_|  __/| (_| || | | | | ||  _|| (_| || |   | | | | | |
// /_/   \_\|_|   \___||_| |_||_||____/  \__|\___| \__,_||_| |_| |_||_|   \__,_||_|   |_| |_| |_|
// |
// Copyright 2015-2020 Łukasz "JustArchi" Domeradzki
// Contact: JustArchi@JustArchi.net
// |
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// |
// http://www.apache.org/licenses/LICENSE-2.0
// |
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArchiSteamFarm.IPC.Controllers.Api;
using ArchiSteamFarm.IPC.Responses;
using ArchiSteamFarm.Steam;
using ArchiSteamFarm.Steam.Data;
using ArchiSteamFarm.Steam.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SteamKit2;

namespace DepositPlugin {
	[Route("/Deposit")]
	public sealed class InventoryController : ArchiController {
		[HttpGet]
		[Route("GetInventory")]
		[ProducesResponseType(typeof(GenericResponse<string>), (int) HttpStatusCode.OK)]
		public ActionResult<GenericResponse> GetInventory() {
			var inventory = DepositPlugin.GetInventory();
			var result = JsonConvert.SerializeObject(inventory);
			return Ok(result);
		}

		[HttpGet]
		[Route("SendTradeOffer")]
		[ProducesResponseType(typeof(GenericResponse<string>), (int) HttpStatusCode.OK)]
		[ProducesResponseType(typeof(GenericResponse), (int) HttpStatusCode.ServiceUnavailable)]
		public async Task<ActionResult<GenericResponse>> SendTradeOffer(string method, string data, string tradeLink) {
			var bot = DepositPlugin.storeBot;

			if (bot == null) {
				return BadRequest("Internal error");
			}
			Regex regex = new Regex(@"steamcommunity.com\/tradeoffer\/new\/\?partner=(\d+)\&token=(.{8}$)");
			var matches = regex.Match(tradeLink);
			var groups = matches.Groups;
			if (!matches.Success || groups.Count < 3) {
				return BadRequest("Invalide trade link");
			}

			if (!uint.TryParse(groups[1].Value, out uint id)) {
				return BadRequest("Invalide trade link");
			}


			var userToken = groups[2].Value;
			SteamID userSteamID = new SteamID(id, EUniverse.Public, EAccountType.Individual);
			var items = JsonConvert.DeserializeObject<List<ulong>>(data);

			if (items == null) {
				return BadRequest("Internal error");
			}

			(bool Success, HashSet<ulong> MobileTradeOfferIDs, string tradeOfferId) result;
			result.Item1 = false;
			result.Item2 = null;
			result.Item3 = "Internal error";
			if ("userSell".Equals(method, StringComparison.OrdinalIgnoreCase)) {
				var userInv = await bot.ArchiWebHandler.GetInventoryAsync(appID: 440, contextID: 2, steamID: userSteamID.ConvertToUInt64()).Where(item => item.Tradable && items.Contains(item.AssetID))
					.ToHashSetAsync()
					.ConfigureAwait(false);

				if (userInv.Count != items.Count) {
					return BadRequest("Internal error");
				}

				result = await bot.ArchiWebHandler.SendTradeOffer(userSteamID.ConvertToUInt64(), itemsToGive: new List<Asset>(), itemsToReceive: userInv, token: userToken, forcedSingleOffer: true, itemsPerTrade: 4000).ConfigureAwait(false);
			} else if ("userBuy".Equals(method, StringComparison.OrdinalIgnoreCase)) {
				var botInv = await bot.ArchiWebHandler.GetInventoryAsync(appID: 440, contextID: 2, steamID: bot.SteamID).Where(item => item.Tradable)
					.ToHashSetAsync()
					.ConfigureAwait(false);
				botInv = botInv.Where(x => items.Any(y => y == x.AssetID)).ToHashSet();
				if (botInv.Count != items.Count) {
					return BadRequest("Internal error");
				}

				result = await bot.ArchiWebHandler.SendTradeOffer(userSteamID.ConvertToUInt64(), itemsToGive: botInv, itemsToReceive: new List<Asset>(), token: userToken, forcedSingleOffer: true, itemsPerTrade: 4000).ConfigureAwait(false);
			}


			if (!result.Success && (result.MobileTradeOfferIDs == null || result.MobileTradeOfferIDs.Count == 0)) {
				return BadRequest("Failed to send a trade offer");
			}

			if (result.MobileTradeOfferIDs.Count != 0)
			{
				var confirmations = await bot.Actions.HandleTwoFactorAuthenticationConfirmations(true, Confirmation.EType.Trade, result.MobileTradeOfferIDs, true).ConfigureAwait(false);
			}

			
			return Ok(result.tradeOfferId);
		}
	}
}
