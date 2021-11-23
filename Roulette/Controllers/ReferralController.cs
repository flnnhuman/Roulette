using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roulette.Context;
using Roulette.Models;
using Roulette.Models.dto;

namespace Roulette.Controllers
{
	public class ReferralController : Controller
	{
		private readonly AppDbContext AppDbContext;

		public ReferralController(AppDbContext context)
		{
			AppDbContext = context;
		}
		
		public async Task<ReferralModel> GetReferralByKey(string key)
		{
			return await  AppDbContext.ReferralModels.FindAsync(key);
		}
		
		public async Task<ReferralModel> GetReferralBySteamId(string key)
		{
			return await AppDbContext.ReferralModels.FirstOrDefaultAsync(x => x.SteamUsersId == key);
		}
		
		public async Task<UseCodeDto> UseCode(string steamId, string key)
		{
			var referral = await AppDbContext.ReferralModels.FirstOrDefaultAsync(x => x.Name == key);
			if (referral is null)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Code was not found"
				};
			}
			var parentUser = await AppDbContext.SteamUsers.FirstOrDefaultAsync(x => x.Referral == referral);
			if (parentUser is null)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Please try to use another code"
				};
			}
			var user = AppDbContext.SteamUsers.Find(steamId);
			if (user is null)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Specified user was not found"
				};
			}

			if (user.Referral == referral)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "User can not use his own code"
				};
			}
			if (user.ParentReferral == null)
			{
				user.ParentReferral = referral;
				referral.Usages += 1;
				user.Balance += referral.Amount;
				parentUser.Balance += referral.Amount;
				
				await AppDbContext.SaveChangesAsync();
				return new UseCodeDto()
				{
					Success = true,
					Referral = referral
				};
			}
			else
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "User already applied a code"
				};
			}
		}

		public async Task<UseCodeDto> EditCodeName(string steamId, string key)
		{
			var referral = await AppDbContext.ReferralModels.FirstOrDefaultAsync(x => x.SteamUsersId == steamId);
			if (referral is null)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Code was not found"
				};
			}

			referral.Name = key;
			return new UseCodeDto()
			{
				Success = true,
				Referral = referral
			};
		}
		
		public async Task<UseCodeDto> CreateCode(string steamId, string key)
		{
			if (await AppDbContext.ReferralModels.AnyAsync(x => x.Name == key))
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Code is already taken"
				};
			}

			var referral = await AppDbContext.ReferralModels.FirstOrDefaultAsync(x => x.SteamUsersId == steamId);
			if (referral is null)
			{
				return new UseCodeDto()
				{
					Success = false,
					Message = "Error oqured, please contact support"
				};
			}

			referral.Name = key;
			await AppDbContext.SaveChangesAsync();
			return new UseCodeDto()
			{
				Success = true,
				Referral = referral
			};
		}
	}
}