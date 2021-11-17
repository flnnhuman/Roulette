using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Roulette.Models
{
    public class Steam
    {
        	public sealed class Asset {
			
			public const uint SteamAppID = 753;
			
			public const ulong SteamCommunityContextID = 6;

			[JsonIgnore]
			public IReadOnlyDictionary<string, JToken> AdditionalPropertiesReadOnly => AdditionalProperties;

			
			public uint Amount { get; internal set; }

			[JsonProperty(PropertyName = "appid", Required = Required.DisallowNull)]
			public uint AppID { get; private set; }
			
			public ulong AssetID { get; private set; }
			
			public ulong ClassID { get; private set; }
			
			public ulong ContextID { get; private set; }
			public ulong InstanceID { get; private set; }
			
			public bool Marketable { get; internal set; }
			
			public ERarity Rarity { get; internal set; }
			
			public uint RealAppID { get; internal set; }
			
			public ImmutableHashSet<Tag> Tags { get; internal set; }
			
			public bool Tradable { get; internal set; }
			
			public EType Type { get; internal set; }

			[JsonExtensionData]
			internal Dictionary<string, JToken> AdditionalProperties { private get; set; }

			[JsonProperty(PropertyName = "amount", Required = Required.Always)]
			private string AmountText {
				get => Amount.ToString(CultureInfo.InvariantCulture);

				set {
					if (string.IsNullOrEmpty(value)) {
						//ASF.ArchiLogger.LogNullError(nameof(value));

						return;
					}

					if (!uint.TryParse(value, out uint amount) || (amount == 0)) {
						//ASF.ArchiLogger.LogNullError(nameof(amount));

						return;
					}

					Amount = amount;
				}
			}

			[JsonProperty(PropertyName = "assetid", Required = Required.DisallowNull)]
			private string AssetIDText {
				get => AssetID.ToString(CultureInfo.InvariantCulture);

				set {
					if (string.IsNullOrEmpty(value)) {
						//ASF.ArchiLogger.LogNullError(nameof(value));

						return;
					}

					if (!ulong.TryParse(value, out ulong assetID) || (assetID == 0)) {
						//ASF.ArchiLogger.LogNullError(nameof(assetID));

						return;
					}

					AssetID = assetID;
				}
			}

			[JsonProperty(PropertyName = "classid", Required = Required.DisallowNull)]
			private string ClassIDText {
				set {
					if (string.IsNullOrEmpty(value)) {
						//ASF.ArchiLogger.LogNullError(nameof(value));

						return;
					}

					if (!ulong.TryParse(value, out ulong classID) || (classID == 0)) {
						return;
					}

					ClassID = classID;
				}
			}

			[JsonProperty(PropertyName = "contextid", Required = Required.DisallowNull)]
			private string ContextIDText {
				get => ContextID.ToString(CultureInfo.InvariantCulture);

				set {
					if (string.IsNullOrEmpty(value)) {
						//ASF.ArchiLogger.LogNullError(nameof(value));

						return;
					}

					if (!ulong.TryParse(value, out ulong contextID) || (contextID == 0)) {
						//ASF.ArchiLogger.LogNullError(nameof(contextID));

						return;
					}

					ContextID = contextID;
				}
			}

			[JsonProperty(PropertyName = "id", Required = Required.DisallowNull)]
			private string IDText {
				set => AssetIDText = value;
			}

			[JsonProperty(PropertyName = "instanceid", Required = Required.DisallowNull)]
			private string InstanceIDText {
				set {
					if (string.IsNullOrEmpty(value)) {
						return;
					}

					if (!ulong.TryParse(value, out ulong instanceID)) {
						//ASF.ArchiLogger.LogNullError(nameof(instanceID));

						return;
					}

					InstanceID = instanceID;
				}
			}

			// Constructed from trades being received or plugins
			public Asset(uint appID, ulong contextID, ulong classID, uint amount, ulong instanceID = 0, ulong assetID = 0, bool marketable = true, bool tradable = true, ImmutableHashSet<Tag> tags = null, uint realAppID = 0, EType type = EType.Unknown, ERarity rarity = ERarity.Unknown) {
				if (appID == 0) {
					throw new ArgumentOutOfRangeException(nameof(appID));
				}

				if (contextID == 0) {
					throw new ArgumentOutOfRangeException(nameof(contextID));
				}

				if (classID == 0) {
					throw new ArgumentOutOfRangeException(nameof(classID));
				}

				if (amount == 0) {
					throw new ArgumentOutOfRangeException(nameof(amount));
				}

				AppID = appID;
				ContextID = contextID;
				ClassID = classID;
				Amount = amount;
				InstanceID = instanceID;
				AssetID = assetID;
				Marketable = marketable;
				Tradable = tradable;
				RealAppID = realAppID;
				Type = type;
				Rarity = rarity;

				if (tags.Count > 0) {
					Tags = tags;
				}
			}

			[JsonConstructor]
			public Asset() { }

			internal Asset CreateShallowCopy() => (Asset) MemberwiseClone();

			public sealed class Tag {
				[JsonProperty(PropertyName = "category", Required = Required.Always)]
				
				public string Identifier { get; private set; } = "";

				[JsonProperty(PropertyName = "internal_name", Required = Required.Always)]
				
				public string Value { get; private set; } = "";

				internal Tag(string identifier, string value) {
					Identifier = !string.IsNullOrEmpty(identifier) ? identifier : throw new ArgumentNullException(nameof(identifier));
					Value = value ?? throw new ArgumentNullException(nameof(value));
				}

				[JsonConstructor]
				private Tag() { }
			}

			public enum ERarity : byte {
				Unknown,
				Common,
				Uncommon,
				Rare
			}

			public enum EType : byte {
				Unknown,
				BoosterPack,
				Emoticon,
				FoilTradingCard,
				ProfileBackground,
				TradingCard,
				SteamGems,
				SaleItem,
				Consumable,
				ProfileModifier,
				Sticker,
				ChatEffect,
				MiniProfileBackground,
				AvatarProfileFrame,
				AnimatedAvatar
			}
		}
            public class Item
            {
	            public ulong id { get; set; }
	            public ulong classid { get; set; }
	            public ItemDescription Description { get; set; }
	            public float Price { get; set; }
            }

            public class ItemDescription
            {
	            public string name { get; set; }
	            public string type { get; set; }
	            public bool tradable { get; set; }
	            public bool marketable { get; set; }
	            public string iconURL { get; set; }
	            public string exterior { get; set; }
	            public string nameColor { get; set; }

	            public string market_name { get; set; }

	            public dynamic metadata { get; set; }
            }
	    
    }
}