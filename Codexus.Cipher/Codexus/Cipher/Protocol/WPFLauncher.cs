using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Codexus.Cipher.Entities;
using Codexus.Cipher.Entities.MPay;
using Codexus.Cipher.Entities.WPFLauncher;
using Codexus.Cipher.Entities.WPFLauncher.Minecraft;
using Codexus.Cipher.Entities.WPFLauncher.Minecraft.Mods;
using Codexus.Cipher.Entities.WPFLauncher.NetGame;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Skin;
using Codexus.Cipher.Entities.WPFLauncher.NetGame.Texture;
using Codexus.Cipher.Entities.WPFLauncher.RentalGame;
using Codexus.Cipher.Utils;
using Codexus.Cipher.Utils.Cipher;
using Codexus.Cipher.Utils.Http;
using Serilog;

namespace Codexus.Cipher.Protocol;

// Token: 0x02000026 RID: 38
public class WPFLauncher : IDisposable
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00005AF8 File Offset: 0x00003CF8
	private static HttpWrapper Http
	{
		get
		{
			var flag = _http == null;
			if (flag)
			{
				var httpLock = HttpLock;
				lock (httpLock)
				{
					var flag3 = _http == null;
					if (flag3)
					{
						_http = new HttpWrapper();
					}
				}
			}
			return _http;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060000EF RID: 239 RVA: 0x00005B70 File Offset: 0x00003D70
	public MPay MPay { get; }

	// Token: 0x060000F0 RID: 240 RVA: 0x00005B78 File Offset: 0x00003D78
	public WPFLauncher()
	{
		var gameVersionWithRetry = GetGameVersionWithRetry();
		MPay = new MPay("aecfrxodyqaaaajp-g-x19", gameVersionWithRetry);
		var userAgent = "WPFLauncher/" + MPay.GameVersion;
		_client = new HttpWrapper("https://x19mclobt.nie.netease.com", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent(userAgent);
		});
		_core = new HttpWrapper("https://x19obtcore.nie.netease.com:8443", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent(userAgent);
		});
		_game = new HttpWrapper("https://x19apigatewayobt.nie.netease.com", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent(userAgent);
		});
		_gateway = new HttpWrapper("https://x19apigatewayobt.nie.netease.com", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent(userAgent);
		});
		_rental = new HttpWrapper("https://x19mclobt.nie.netease.com", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent(userAgent);
		});
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00005C6C File Offset: 0x00003E6C
	private static string GetGameVersionWithRetry()
	{
		string text;
		try
		{
			text = GetLatestVersionAsync().GetAwaiter().GetResult();
		}
		catch (ObjectDisposedException)
		{
			var httpLock = HttpLock;
			lock (httpLock)
			{
				var http = _http;
				http?.Dispose();
				_http = new HttpWrapper();
			}
			text = GetLatestVersionAsync().GetAwaiter().GetResult();
		}
		return text;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00005D08 File Offset: 0x00003F08
	public void Dispose()
	{
		var httpLock = HttpLock;
		lock (httpLock)
		{
			var http = _http;
			http?.Dispose();
		}
		_core.Dispose();
		_game.Dispose();
		MPay.Dispose();
		_gateway.Dispose();
		_client.Dispose();
		_rental.Dispose();
		GC.SuppressFinalize(this);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00005DA8 File Offset: 0x00003FA8
	public static string GetUserAgent()
	{
		string text;
		try
		{
			text = "WPFLauncher/" + GetLatestVersionAsync().GetAwaiter().GetResult();
		}
		catch (ObjectDisposedException)
		{
			var httpLock = HttpLock;
			lock (httpLock)
			{
				var http = _http;
				http?.Dispose();
				_http = new HttpWrapper();
			}
			text = "WPFLauncher/" + GetLatestVersionAsync().GetAwaiter().GetResult();
		}
		return text;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00005E58 File Offset: 0x00004058
	// private static async Task<Dictionary<string, EntityPatchVersion>> GetPatchVersionsAsync()
	// {
	// 	var num = 0;
	// 	try
	// 	{
	// 		var httpResponseMessage = await Http.GetAsync("https://x19.update.netease.com/pl/x19_java_patchlist");
	// 		var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
	// 		var text = text2;
	// 		return JsonSerializer.Deserialize<Dictionary<string, EntityPatchVersion>>("{" + text[..text.LastIndexOf(',')] + "}");
	// 	}
	// 	catch (ObjectDisposedException obj)
	// 	{
	// 		num = 1;
	// 	}
	// 	if (num != 1)
	// 	{
	// 		throw null;
	// 	}
	// 	var obj2 = _httpLock;
	// 	lock (obj2)
	// 	{
	// 		var http = _http;
	// 		http?.Dispose();
	// 		_http = new HttpWrapper();
	// 	}
	// 	obj2 = null;
	// 	var httpResponseMessage2 = await Http.GetAsync("https://x19.update.netease.com/pl/x19_java_patchlist");
	// 	var text3 = await httpResponseMessage2.Content.ReadAsStringAsync();
	// 	httpResponseMessage2 = null;
	// 	var textRetry = text3;
	// 	text3 = null;
	// 	return JsonSerializer.Deserialize<Dictionary<string, EntityPatchVersion>>("{" + textRetry.Substring(0, textRetry.LastIndexOf(',')) + "}");
	// }
	private static async Task<Dictionary<string, EntityPatchVersion>> GetPatchVersionsAsync()
	{
		const string url = "https://x19.update.netease.com/pl/x19_java_patchlist";

		try
		{
			return await FetchAndParseAsync(url);
		}
		catch (ObjectDisposedException)
		{
			// Re-initialize the global HTTP client if it was disposed
			lock (HttpLock)
			{
				_http?.Dispose();
				_http = new HttpWrapper();
			}

			// Retry once after re-initialization
			return await FetchAndParseAsync(url);
		}
	}

	private static async Task<Dictionary<string, EntityPatchVersion>> FetchAndParseAsync(string url)
	{
		var response = await Http.GetAsync(url);
		var rawText = await response.Content.ReadAsStringAsync();

		// The server returns a JSON fragment with a trailing comma (e.g., "key":{...},)
		// We wrap it in braces and remove that last comma to make it valid JSON.
		var lastComma = rawText.LastIndexOf(',');
		var validJson = "{" + rawText[..lastComma] + "}";

		return JsonSerializer.Deserialize<Dictionary<string, EntityPatchVersion>>(validJson)!;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00005E98 File Offset: 0x00004098
	public static async Task<string> GetLatestVersionAsync()
	{
		var dictionary = await GetPatchVersionsAsync();
		return dictionary.Keys.Last();
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00005ED8 File Offset: 0x000040D8
	public async Task<EntityMPayUserResponse> LoginWithEmailAsync(string email, string password)
	{
		return await MPay.LoginWithEmailAsync(email, password);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00005F2C File Offset: 0x0000412C
	public static EntityX19CookieRequest GenerateCookie(EntityMPayUserResponse user, EntityDevice device)
	{
		return new EntityX19CookieRequest
		{
			Json = JsonSerializer.Serialize(new EntityX19Cookie
			{
				SdkUid = user.User.Id,
				SessionId = user.User.Token,
				Udid = Guid.NewGuid().ToString("N").ToUpper(),
				DeviceId = device.Id,
				AimInfo = "{\"aim\":\"127.0.0.1\",\"country\":\"CN\",\"tz\":\"+0800\",\"tzid\":\"\"}"
			}, DefaultOptions)
		};
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00005FB4 File Offset: 0x000041B4
	public ValueTuple<EntityAuthenticationOtp, string> LoginWithCookie(string cookie)
	{
		return LoginWithCookieAsync(cookie).GetAwaiter().GetResult();
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00005FDC File Offset: 0x000041DC
	public ValueTuple<EntityAuthenticationOtp, string> LoginWithCookie(EntityX19CookieRequest cookie)
	{
		return LoginWithCookieAsync(cookie).GetAwaiter().GetResult();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00006004 File Offset: 0x00004204
	private async Task<ValueTuple<EntityAuthenticationOtp, string>> LoginWithCookieAsync(string cookie)
	{
		EntityX19CookieRequest cookie2;
		try
		{
			cookie2 = JsonSerializer.Deserialize<EntityX19CookieRequest>(cookie);
		}
		catch (Exception)
		{
			cookie2 = new EntityX19CookieRequest
			{
				Json = cookie
			};
		}
		return await LoginWithCookieAsync(cookie2);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00006050 File Offset: 0x00004250
	private async Task<ValueTuple<EntityAuthenticationOtp, string>> LoginWithCookieAsync(EntityX19CookieRequest cookie)
	{
		var entity = JsonSerializer.Deserialize<EntityX19Cookie>(cookie.Json);
		var flag = entity.LoginChannel != "netease";
		if (flag)
		{
			await _sdk.AuthSession(cookie.Json);
		}
		Log.Debug("Your Cookie: {Cookie}", cookie.Json);
		Log.Information("Login with Cookie...");
		var entityLoginOtp = await LoginOtpAsync(cookie);
		var entityAuthenticationOtp = await AuthenticationOtpAsync(cookie, entityLoginOtp);
		await InterConn.LoginStart(entityAuthenticationOtp.EntityId, entityAuthenticationOtp.Token);
		_ = Task.Run(async delegate
		{
			await Http.GetAsync(string.Concat(new[]
			{
				"https://service.codexus.today/interconnection/report?id=",
				entityAuthenticationOtp.EntityId,
				"&token=",
				entityAuthenticationOtp.Token,
				"&version=",
				MPay.GameVersion
			}));
		});
		return new ValueTuple<EntityAuthenticationOtp, string>(entityAuthenticationOtp, entity.LoginChannel);
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000609C File Offset: 0x0000429C
	private async Task<EntityLoginOtp> LoginOtpAsync(EntityX19CookieRequest cookieRequest)
	{
		var httpResponseMessage = await _core.PostAsync("/login-otp", JsonSerializer.Serialize(cookieRequest, DefaultOptions));
		var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
		var entity = JsonSerializer.Deserialize<Entity<JsonElement?>>(text2);
		if (entity == null)
		{
			throw new Exception("Failed to deserialize: " + text2);
		}
		if (entity.Code != 0 || entity.Data == null)
		{
			throw new Exception("Failed to deserialize: " + entity.Message);
		}
		return JsonSerializer.Deserialize<EntityLoginOtp>(entity.Data.Value.GetRawText());
	}

	// Token: 0x060000FD RID: 253 RVA: 0x000060E8 File Offset: 0x000042E8
	private async Task<EntityAuthenticationOtp> AuthenticationOtpAsync(EntityX19CookieRequest cookieRequest, EntityLoginOtp otp)
	{
		var entityX19Cookie = JsonSerializer.Deserialize<EntityX19Cookie>(cookieRequest.Json);
		var text = StringGenerator.GenerateHexString(4).ToUpper();
		var value = new EntityAuthenticationDetail
		{
			Udid = "0000000000000000" + text,
			AppVersion = MPay.GameVersion,
			PayChannel = entityX19Cookie.AppChannel,
			Disk = text
		};
		var s = JsonSerializer.Serialize(new EntityAuthenticationData
		{
			SaData = JsonSerializer.Serialize(value, DefaultOptions),
			AuthJson = cookieRequest.Json,
			Version = new EntityAuthenticationVersion
			{
				Version = MPay.GameVersion
			},
			Aid = otp.Aid.ToString(),
			OtpToken = otp.OtpToken,
			LockTime = 0
		}, DefaultOptions);
		var body = HttpUtil.HttpEncrypt(Encoding.UTF8.GetBytes(s));
		var httpResponseMessage = await _core.PostAsync("/authentication-otp", body);
		var array = await httpResponseMessage.Content.ReadAsByteArrayAsync();
		var array2 = HttpUtil.HttpDecrypt(array);
		if (array2 == null)
		{
			throw new Exception("Cannot decrypt data");
		}
		var entity = JsonSerializer.Deserialize<Entity<EntityAuthenticationOtp>>(array2);
		return entity.Code == 0 ? entity.Data : throw new Exception(entity.Message);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000613C File Offset: 0x0000433C
	public Entities<EntityNetGameItem> GetAvailableNetGames(string userId, string userToken, int offset, int length)
	{
		return GetAvailableNetGamesAsync(userId, userToken, offset, length).GetAwaiter().GetResult();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006168 File Offset: 0x00004368
	private async Task<Entities<EntityNetGameItem>> GetAvailableNetGamesAsync(string userId, string userToken, int offset, int length)
	{
		var body = JsonSerializer.Serialize(new EntityNetGameRequest
		{
			AvailableMcVersions = [],
			ItemType = 1,
			Length = length,
			Offset = offset,
			MasterTypeId = "2",
			SecondaryTypeId = ""
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/item/query/available", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityNetGameItem>>(text);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000061CC File Offset: 0x000043CC
	public Entities<EntityQueryNetGameItem> QueryNetGameItemByIds(string userId, string userToken, string[] ids)
	{
		return QueryNetGameItemByIdsAsync(userId, userToken, ids).GetAwaiter().GetResult();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000061F4 File Offset: 0x000043F4
	private async Task<Entities<EntityQueryNetGameItem>> QueryNetGameItemByIdsAsync(string userId, string userToken, string[] ids)
	{
		var body = JsonSerializer.Serialize(new EntityQueryNetGameRequest
		{
			EntityIds = ids
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/item/query/search-by-ids", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityQueryNetGameItem>>(text);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00006250 File Offset: 0x00004450
	public Entity<EntityQueryNetGameDetailItem> QueryNetGameDetailById(string userId, string userToken, string gameId)
	{
		return QueryNetGameDetailByIdAsync(userId, userToken, gameId).GetAwaiter().GetResult();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00006278 File Offset: 0x00004478
	private async Task<Entity<EntityQueryNetGameDetailItem>> QueryNetGameDetailByIdAsync(string userId, string userToken, string gameId)
	{
		var body = JsonSerializer.Serialize(new EntityQueryNetGameDetailRequest
		{
			ItemId = gameId
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/item-details/get_v2", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityQueryNetGameDetailItem>>(text);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000062D4 File Offset: 0x000044D4
	public Entities<EntityGameCharacter> QueryNetGameCharacters(string userId, string userToken, string gameId)
	{
		return QueryNetGameCharactersAsync(userId, userToken, gameId).GetAwaiter().GetResult();
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000062FC File Offset: 0x000044FC
	private async Task<Entities<EntityGameCharacter>> QueryNetGameCharactersAsync(string userId, string userToken, string gameId)
	{
		var body = JsonSerializer.Serialize(new EntityQueryGameCharacters
		{
			GameId = gameId,
			UserId = userId
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/game-character/query/user-game-characters", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityGameCharacter>>(text);
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006358 File Offset: 0x00004558
	public Entity<EntityNetGameServerAddress> GetNetGameServerAddress(string userId, string userToken, string gameId)
	{
		return GetNetGameServerAddressAsync(userId, userToken, gameId).GetAwaiter().GetResult();
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00006380 File Offset: 0x00004580
	private async Task<Entity<EntityNetGameServerAddress>> GetNetGameServerAddressAsync(string userId, string userToken, string gameId)
	{
		var body = JsonSerializer.Serialize(new
		{
			item_id = gameId
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/item-address/get", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityNetGameServerAddress>>(text);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000063DC File Offset: 0x000045DC
	public async Task<Entity<EntityQuerySearchByGameResponse>> GetGameCoreModListAsync(string userId, string userToken, EnumGameVersion gameVersion, bool isRental)
	{
		var body = JsonSerializer.Serialize(new EntityQuerySearchByGameRequest
		{
			McVersionId = (int)gameVersion,
			GameType = isRental ? 8 : 2
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/game-auth-item-list/query/search-by-game", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityQuerySearchByGameResponse>>(text);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00006440 File Offset: 0x00004640
	public async Task<Entities<EntityComponentDownloadInfoResponse>> GetGameCoreModDetailsListAsync(string userId, string userToken, List<ulong> gameModList)
	{
		var body = JsonSerializer.Serialize(new EntitySearchByIdsQuery
		{
			ItemIdList = gameModList
		}, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/user-item-download-v2/get-list", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityComponentDownloadInfoResponse>>(text);
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000649C File Offset: 0x0000469C
	public Entities<EntitySkin> GetFreeSkinList(string userId, string userToken, int offset, int length = 20)
	{
		return GetFreeSkinListAsync(userId, userToken, offset, length).GetAwaiter().GetResult();
	}

	// Token: 0x0600010B RID: 267 RVA: 0x000064C8 File Offset: 0x000046C8
	private async Task<Entities<EntitySkin>> GetFreeSkinListAsync(string userId, string userToken, int offset, int length = 20)
	{
		var body = JsonSerializer.Serialize(new EntityFreeSkinListRequest
		{
			IsHas = true,
			ItemType = 2,
			Length = length,
			MasterTypeId = 10,
			Offset = offset,
			PriceType = 3,
			SecondaryTypeId = 31
		}, DefaultOptions);
		var httpResponseMessage = await _gateway.PostAsync("/item/query/available", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntitySkin>>(text);
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000652C File Offset: 0x0000472C
	public Entities<EntitySkin> QueryFreeSkinByName(string userId, string userToken, string name)
	{
		return QueryFreeSkinByNameAsync(userId, userToken, name).GetAwaiter().GetResult();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00006554 File Offset: 0x00004754
	private async Task<Entities<EntitySkin>> QueryFreeSkinByNameAsync(string userId, string userToken, string name)
	{
		var body = JsonSerializer.Serialize(new EntityQuerySkinByNameRequest
		{
			IsHas = true,
			IsSync = 0,
			ItemType = 2,
			Keyword = name,
			Length = 20,
			MasterTypeId = 10,
			Offset = 0,
			PriceType = 3,
			SecondaryTypeId = "31",
			SortType = 1,
			Year = 0
		}, DefaultOptions);
		var httpResponseMessage = await _gateway.PostAsync("/item/query/search-by-keyword", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntitySkin>>(text);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000065B0 File Offset: 0x000047B0
	public Entities<EntitySkin> GetSkinDetails(string userId, string userToken, Entities<EntitySkin> skinList)
	{
		return GetSkinDetailsAsync(userId, userToken, skinList).GetAwaiter().GetResult();
	}

	// Token: 0x0600010F RID: 271 RVA: 0x000065D8 File Offset: 0x000047D8
	private async Task<Entities<EntitySkin>> GetSkinDetailsAsync(string userId, string userToken, Entities<EntitySkin> skinList)
	{
		var entityIds = skinList.Data.Select(e => e.EntityId).ToList();
		var body = JsonSerializer.Serialize(new EntitySkinDetailsRequest
		{
			ChannelId = 11,
			EntityIds = entityIds,
			IsHas = true,
			WithPrice = true,
			WithTitleImage = true
		}, DefaultOptions);
		var httpResponseMessage = await _gateway.PostAsync("/item/query/search-by-ids", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntitySkin>>(text);
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00006634 File Offset: 0x00004834
	public EntityResponse PurchaseSkin(string userId, string userToken, string entityId)
	{
		return PurchaseSkinAsync(userId, userToken, entityId).GetAwaiter().GetResult();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000665C File Offset: 0x0000485C
	private async Task<EntityResponse> PurchaseSkinAsync(string userId, string userToken, string entityId)
	{
		var body = JsonSerializer.Serialize(new EntitySkinPurchaseRequest
		{
			BatchCount = 1,
			BuyPath = "PC_H5_COMPONENT_DETAIL",
			Diamond = 0,
			EntityId = 0,
			ItemId = entityId,
			ItemLevel = 0,
			LastPlayTime = 0,
			PurchaseTime = 0,
			TotalPlayTime = 0,
			UserId = userId
		}, DefaultOptions);
		var httpResponseMessage = await _gateway.PostAsync("/user-item-purchase", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<EntityResponse>(text);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000066B8 File Offset: 0x000048B8
	public EntityResponse SetSkin(string userId, string userToken, string entityId)
	{
		return SetSkinAsync(userId, userToken, entityId).GetAwaiter().GetResult();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x000066E0 File Offset: 0x000048E0
	private async Task<EntityResponse> SetSkinAsync(string userId, string userToken, string entityId)
	{
		var body = JsonSerializer.Serialize(new
		{
			skin_settings = new List<EntitySkinSettings>
			{
				new()
				{
					ClientType = "java",
					GameType = 9,
					SkinId = entityId,
					SkinMode = 0,
					SkinType = 31
				},
				new()
				{
					ClientType = "java",
					GameType = 8,
					SkinId = entityId,
					SkinMode = 0,
					SkinType = 31
				},
				new()
				{
					ClientType = "java",
					GameType = 2,
					SkinId = entityId,
					SkinMode = 0,
					SkinType = 31
				},
				new()
				{
					ClientType = "java",
					GameType = 10,
					SkinId = entityId,
					SkinMode = 0,
					SkinType = 31
				},
				new()
				{
					ClientType = "java",
					GameType = 7,
					SkinId = entityId,
					SkinMode = 0,
					SkinType = 31
				}
			}
		}, DefaultOptions);
		var httpResponseMessage = await _gateway.PostAsync("/user-game-skin-multi", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<EntityResponse>(text);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000673C File Offset: 0x0000493C
	public List<EntityUserGameTexture> GetSkinListInGame(string userId, string userToken, EntityUserGameTextureRequest entity)
	{
		return GetSkinListInGameAsync(userId, userToken, entity).GetAwaiter().GetResult();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00006764 File Offset: 0x00004964
	private async Task<List<EntityUserGameTexture>> GetSkinListInGameAsync(string userId, string userToken, EntityUserGameTextureRequest entity)
	{
		var body = JsonSerializer.Serialize(entity, DefaultOptions);
		var httpResponseMessage = await _game.PostAsync("/user-game-skin/query/search-by-type", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityUserGameTexture>>(text).Data.ToList();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x000067C0 File Offset: 0x000049C0
	public void CreateCharacter(string userId, string userToken, string gameId, string roleName)
	{
		CreateCharacterAsync(userId, userToken, gameId, roleName).GetAwaiter().GetResult();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000067E8 File Offset: 0x000049E8
	private async Task CreateCharacterAsync(string userId, string userToken, string gameId, string roleName)
	{
		var httpResponseMessage = await _game.PostAsync("/game-character", JsonSerializer.Serialize(new EntityCreateCharacter
		{
			GameId = gameId,
			UserId = userId,
			Name = roleName
		}, DefaultOptions), delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var flag = !httpResponseMessage.IsSuccessStatusCode;
		if (flag)
		{
				throw new Exception("Failed to create character");
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000684C File Offset: 0x00004A4C
	public async Task<EntityAuthenticationUpdate> AuthenticationUpdateAsync(string userId, string userToken)
	{
		var entity = JsonSerializer.Serialize(new EntityAuthenticationUpdate
		{
			EntityId = userId,
			IsRegister = true
		}, DefaultOptions);
		var body = HttpUtil.HttpEncrypt(Encoding.UTF8.GetBytes(entity));
		var httpResponseMessage = await _core.PostAsync("/authentication/update", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, entity, userId, userToken));
		});
		var array2 = await httpResponseMessage.Content.ReadAsByteArrayAsync();
		var array = HttpUtil.HttpDecrypt(array2);
		array2 = null;
		if (httpResponseMessage.IsSuccessStatusCode)
		{
			try
			{
				return JsonSerializer.Deserialize<Entity<EntityAuthenticationUpdate>>(array).Data;
			}
			catch (Exception ex)
			{
				Log.Error("Exception while deserializing, reason: {Message}", ex.Message);
			}
		}
		Log.Error("Failed to update authentication, response: {Json}", array);
		return null;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000068A0 File Offset: 0x00004AA0
	public Entities<EntityNetGameItem> QueryNetGameWithKeyword(string userId, string userToken, string keyword)
	{
		return QueryNetGameWithKeywordAsync(userId, userToken, keyword).GetAwaiter().GetResult();
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000068C8 File Offset: 0x00004AC8
	private async Task<Entities<EntityNetGameItem>> QueryNetGameWithKeywordAsync(string userId, string userToken, string keyword)
	{
		var httpResponseMessage = await _game.PostAsync("/item/query/search-by-keyword", JsonSerializer.Serialize(new EntityNetGameKeyword
		{
			Keyword = keyword
		}, DefaultOptions), delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
		Entities<EntityNetGameItem> entities;
		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			Log.Error("Failed to query net game with keyword, response: {Json}", text2);
			entities = null;
		}
		else
		{
			entities = JsonSerializer.Deserialize<Entities<EntityNetGameItem>>(text2);
		}
		return entities;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00006924 File Offset: 0x00004B24
	public Entity<EntityCoreLibResponse> GetMinecraftClientLibs(string userId, string userToken, EnumGameVersion? gameVersion = null)
	{
		return GetMinecraftClientLibsAsync(userId, userToken, gameVersion).GetAwaiter().GetResult();
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000694C File Offset: 0x00004B4C
	private async Task<Entity<EntityCoreLibResponse>> GetMinecraftClientLibsAsync(string userId, string userToken, EnumGameVersion? gameVersion = null)
	{
		gameVersion.GetValueOrDefault();
		var flag = gameVersion == null;
		if (flag)
		{
			var value = EnumGameVersion.NONE;
			gameVersion = value;
		}
		var body = JsonSerializer.Serialize(new EntityMcDownloadVersion
		{
			McVersion = (int)gameVersion.Value
		}, DefaultOptions);
		var httpResponseMessage = await _client.PostAsync("/game-patch-info", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityCoreLibResponse>>(text);
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000069A8 File Offset: 0x00004BA8
	public Entity<EntityComponentDownloadInfoResponse> GetNetGameComponentDownloadList(string userId, string userToken, string gameId)
	{
		return GetNetGameComponentDownloadListAsync(userId, userToken, gameId).GetAwaiter().GetResult();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000069D0 File Offset: 0x00004BD0
	private async Task<Entity<EntityComponentDownloadInfoResponse>> GetNetGameComponentDownloadListAsync(string userId, string userToken, string gameId)
	{
		var body = JsonSerializer.Serialize(new EntitySearchByItemIdQuery
		{
			ItemId = gameId,
			Length = 0,
			Offset = 0
		}, DefaultOptions);
		var httpResponseMessage = await _client.PostAsync("/user-item-download-v2", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityComponentDownloadInfoResponse>>(text);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00006A2C File Offset: 0x00004C2C
	public Entities<EntityRentalGame> GetRentalGameList(string userId, string userToken, int offset)
	{
		return GetRentalGameListAsync(userId, userToken, offset).GetAwaiter().GetResult();
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00006A54 File Offset: 0x00004C54
	private async Task<Entities<EntityRentalGame>> GetRentalGameListAsync(string userId, string userToken, int offset)
	{
		var body = JsonSerializer.Serialize(new EntityQueryRentalGame
		{
			Offset = offset,
			SortType = 0
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server/query/available-public-server", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var jsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
			}
		};
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityRentalGame>>(text, jsonSerializerOptions);
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00006AB0 File Offset: 0x00004CB0
	public Entities<EntityRentalGamePlayerList> GetRentalGameRolesList(string userId, string userToken, string entityId)
	{
		return GetRentalGameRolesListAsync(userId, userToken, entityId).GetAwaiter().GetResult();
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00006AD8 File Offset: 0x00004CD8
	private async Task<Entities<EntityRentalGamePlayerList>> GetRentalGameRolesListAsync(string userId, string userToken, string entityId)
	{
		var body = JsonSerializer.Serialize(new EntityQueryRentalGamePlayerList
		{
			ServerId = entityId,
			Offset = 0,
			Length = 10
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server-player/query/search-by-user-server", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityRentalGamePlayerList>>(text);
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00006B34 File Offset: 0x00004D34
	public Entity<EntityRentalGamePlayerList> AddRentalGameRole(string userId, string userToken, string serverId, string roleName)
	{
		return AddRentalGameRoleAsync(userId, userToken, serverId, roleName).GetAwaiter().GetResult();
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00006B60 File Offset: 0x00004D60
	private async Task<Entity<EntityRentalGamePlayerList>> AddRentalGameRoleAsync(string userId, string userToken, string serverId, string roleName)
	{
		var body = JsonSerializer.Serialize(new EntityAddRentalGameRole
		{
			ServerId = serverId,
			UserId = userId,
			Name = roleName,
			CreateTs = 555555,
			IsOnline = false,
			Status = 0
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server-player", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityRentalGamePlayerList>>(text);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00006BC4 File Offset: 0x00004DC4
	public Entity<EntityRentalGamePlayerList> DeleteRentalGameRole(string userId, string userToken, string entityId)
	{
		return DeleteRentalGameRoleAsync(userId, userToken, entityId).GetAwaiter().GetResult();
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00006BEC File Offset: 0x00004DEC
	private async Task<Entity<EntityRentalGamePlayerList>> DeleteRentalGameRoleAsync(string userId, string userToken, string entityId)
	{
		var body = JsonSerializer.Serialize(new EntityDeleteRentalGameRole
		{
			EntityId = entityId
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server-player/delete", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityRentalGamePlayerList>>(text);
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00006C48 File Offset: 0x00004E48
	public Entity<EntityRentalGameServerAddress> GetRentalGameServerAddress(string userId, string userToken, string entityId,  string pwd = null)
	{
		return GetRentalGameServerAddressAsync(userId, userToken, entityId, pwd).GetAwaiter().GetResult();
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00006C74 File Offset: 0x00004E74
	private async Task<Entity<EntityRentalGameServerAddress>> GetRentalGameServerAddressAsync(string userId, string userToken, string entityId,  string pwd = null)
	{
		var body = JsonSerializer.Serialize(new EntityQueryRentalGameServerAddress
		{
			ServerId = entityId,
			Password = pwd ?? "none"
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server-world-enter/get", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityRentalGameServerAddress>>(text);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00006CD8 File Offset: 0x00004ED8
	public Entity<EntityRentalGameDetails> GetRentalGameDetails(string userId, string userToken, string entityId)
	{
		return GetRentalGameDetailsAsync(userId, userToken, entityId).GetAwaiter().GetResult();
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00006D00 File Offset: 0x00004F00
	private async Task<Entity<EntityRentalGameDetails>> GetRentalGameDetailsAsync(string userId, string userToken, string entityId)
	{
		var body = JsonSerializer.Serialize(new EntityQueryRentalGameDetail
		{
			ServerId = entityId
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server-details/get", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		var json = text;
		text = null;
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
			}
		};
		return JsonSerializer.Deserialize<Entity<EntityRentalGameDetails>>(json, options);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00006D5C File Offset: 0x00004F5C
	public Entities<EntityRentalGame> SearchRentalGameByName(string userId, string userToken, string worldId)
	{
		return SearchRentalGameByNameAsync(userId, userToken, worldId).GetAwaiter().GetResult();
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00006D84 File Offset: 0x00004F84
	private async Task<Entities<EntityRentalGame>> SearchRentalGameByNameAsync(string userId, string userToken, string worldId)
	{
		var body = JsonSerializer.Serialize(new EntityQueryRentalGameById
		{
			Offset = 0UL,
			SortType = EnumSortType.General,
			WorldNameKey = [worldId]
		}, DefaultOptions);
		var httpResponseMessage = await _rental.PostAsync("/rental-server/query/available-public-server", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities<EntityRentalGame>>(text, JsonSerializerOptions);
	}
	
	private static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		Converters = 
		{
			new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
		}
	};

	// Token: 0x0400006F RID: 111
	private static HttpWrapper _http;

	// Token: 0x04000070 RID: 112
	private static readonly object HttpLock = new();

	// Token: 0x04000071 RID: 113
	private readonly HttpWrapper _client;

	// Token: 0x04000072 RID: 114
	private readonly HttpWrapper _core;

	// Token: 0x04000073 RID: 115
	private readonly HttpWrapper _game;

	// Token: 0x04000074 RID: 116
	private readonly HttpWrapper _gateway;

	// Token: 0x04000075 RID: 117
	private readonly HttpWrapper _rental;

	// Token: 0x04000076 RID: 118
	private readonly MgbSdk _sdk = new("x19");

	// Token: 0x04000077 RID: 119
	private static readonly JsonSerializerOptions DefaultOptions = new()
	{
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};
}