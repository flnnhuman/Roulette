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
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Plugins.Interfaces;
using ArchiSteamFarm.Steam;
using ArchiSteamFarm.Steam.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamKit2;

namespace DepositPlugin {
	[Export(typeof(IPlugin))]
	internal sealed class DepositPlugin : IASF, IBot, IBotConnection, IBotModules {
		public string Name => nameof(DepositPlugin);

		public Version Version => typeof(DepositPlugin).Assembly.GetName().Version ?? throw new InvalidOperationException(nameof(Version));
		public static HashSet<Asset> TFInventory = new();
		public static DateTime lastUpdate = DateTime.UtcNow;
		public static Bot storeBot;
		public static SemaphoreSlim inventorySemaphore = new (1, 1);

		[JsonProperty]
		public bool CustomIsEnabledField { get; private set; } = true;

		public void OnASFInit(IReadOnlyDictionary<string, JToken>? additionalConfigProperties = null) {
			if (additionalConfigProperties == null) {
				return;
			}

			foreach (KeyValuePair<string, JToken> configProperty in additionalConfigProperties) {
				switch (configProperty.Key) {
					case nameof(DepositPlugin) + "TestProperty" when configProperty.Value.Type == JTokenType.Boolean:
						bool exampleBooleanValue = configProperty.Value.Value<bool>();
						ASF.ArchiLogger.LogGenericInfo(nameof(DepositPlugin) + "TestProperty boolean property has been found with a value of: " + exampleBooleanValue);

						break;
				}
			}
		}

		public void OnBotDestroy(Bot bot) { }

		public void OnBotDisconnected(Bot bot, EResult reason) {
			TFInventory = null;
		}

		public void OnBotInit(Bot bot) { }

		public async void OnBotInitModules(Bot bot, IReadOnlyDictionary<string, JToken>? additionalConfigProperties = null) {
			await bot.Actions.Pause(true).ConfigureAwait(false);
		}

		public async void OnBotLoggedOn(Bot bot) {
			HashSet<Asset> tfinventory = await bot.ArchiWebHandler.GetInventoryAsync(bot.SteamID, 440, 2).Where(item => item.Tradable).ToHashSetAsync().ConfigureAwait(false);
			TFInventory = tfinventory;
			storeBot = bot;
		}

		public void OnLoaded() { }

		public static async Task<HashSet<Asset>> GetInventory() {
			await inventorySemaphore.WaitAsync().ConfigureAwait(false);
			if (TFInventory.Count == 0) {
				inventorySemaphore.Release();
				return null!;
			}

			if ((DateTime.UtcNow - lastUpdate).TotalMinutes > 5) {
				HashSet<Asset> tfinventory = await storeBot.ArchiWebHandler.GetInventoryAsync(storeBot.SteamID, 440, 2).Where(item => item.Tradable).ToHashSetAsync().ConfigureAwait(false);
				TFInventory = tfinventory;
				lastUpdate = DateTime.UtcNow;
			}

			inventorySemaphore.Release();
			return TFInventory;
		}
	}
}
