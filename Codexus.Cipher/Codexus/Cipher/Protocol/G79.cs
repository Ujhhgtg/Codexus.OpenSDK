using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities;
using Codexus.Cipher.Entities.Connection.G79;
using Codexus.Cipher.Entities.G79;
using Codexus.Cipher.Entities.G79.NetGame;
using Codexus.Cipher.Entities.G79.RentalGame;
using Codexus.Cipher.Entities.WPFLauncher;
using Codexus.Cipher.Utils.Cipher;
using Codexus.Cipher.Utils.Http;
using Codexus.Development.SDK.Utils;
using Serilog;
using EntityAuthenticationOtp = Codexus.Cipher.Entities.G79.EntityAuthenticationOtp;

namespace Codexus.Cipher.Protocol;

// Token: 0x02000020 RID: 32
public class G79 : IDisposable
{
	// Token: 0x060000B2 RID: 178 RVA: 0x000049A8 File Offset: 0x00002BA8
	public void Dispose()
	{
		_core.Dispose();
		_client.Dispose();
		GC.SuppressFinalize(this);
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000049CC File Offset: 0x00002BCC
	public EntityAuthenticationOtp AuthenticationOtp(string cookieRequest, string nexusToken)
	{
		return AuthenticationOtpAsync(cookieRequest, nexusToken).GetAwaiter().GetResult();
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000049F4 File Offset: 0x00002BF4
	private static string ExtractCookie(string cookie)
	{
		string text;
		try
		{
			text = JsonSerializer.Deserialize<EntityX19CookieRequest>(cookie).Json;
		}
		catch (Exception)
		{
			text = cookie;
		}
		return text;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00004A2C File Offset: 0x00002C2C
	private async Task<EntityAuthenticationOtp> AuthenticationOtpAsync(string cookieRequest, string nexusToken)
	{
		var text = ExtractCookie(cookieRequest);
		Log.Information("Try extracting cookie...");
		var flag = cookieRequest.Contains("4399com");
		if (flag)
		{
			_mgbSdk.AuthSession(text).GetAwaiter().GetResult();
		}
		Log.Information("Encrypting cookie...");
		using var api = new WebNexusApi(nexusToken);
		string text4;
		if (!text.StartsWith('{'))
		{
			var text3 = await api.PeHttpEncryptAsync(text);
			text4 = text3;
		}
		else
		{
			var text5 = await api.PeAccountConvert(text);
			text4 = text5;
		}
		var json = text4;
		var body = JsonSerializer.Deserialize<EntityHttpEncrypt>(json).Body;
		var httpResponseMessage = await _core.PostAsync("/pe-authentication", body);
		var text6 = await httpResponseMessage.Content.ReadAsStringAsync();
		Log.Information("Decrypting response...");
		var entity = JsonSerializer.Deserialize<Entity<JsonElement?>>(JsonSerializer.Deserialize<EntityHttpDecrypt>(api.PeHttpDecryptAsync(text6).GetAwaiter().GetResult()).Body);
		if (entity == null)
		{
			throw new Exception("Failed to deserialize: " + text6);
		}
		if (entity.Code != 0 || entity.Data == null)
		{
			throw new Exception("Failed to deserialize: " + entity.Message);
		}
		var entityAuthenticationOtp = JsonSerializer.Deserialize<EntityAuthenticationOtp>(entity.Data.Value.GetRawText());
		return entityAuthenticationOtp;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00004A80 File Offset: 0x00002C80
	public Entity<EntityUserDetails> GetUserDetail(string userId, string userToken)
	{
		return GetUserDetailAsync(userId, userToken).GetAwaiter().GetResult();
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00004AA8 File Offset: 0x00002CA8
	private async Task<Entity<EntityUserDetails>> GetUserDetailAsync(string userId, string userToken)
	{
		var body = JsonSerializer.Serialize(new EntityQueryUserDetail
		{
			Version = new Version(2, 0)
		});
		var httpResponseMessage = await _core.PostAsync("/pe-user-detail/get", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
		var entity = JsonSerializer.Deserialize<Entity<EntityUserDetails>>(text2);
		return entity ?? throw new Exception("Failed to deserialize: " + text2);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00004AFC File Offset: 0x00002CFC
	public Entities.G79.Entities<EntityNetGame> GetAvailableNetGames(string userId, string userToken)
	{
		return GetAvailableNetGamesAsync(userId, userToken).GetAwaiter().GetResult();
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00004B24 File Offset: 0x00002D24
	private async Task<Entities.G79.Entities<EntityNetGame>> GetAvailableNetGamesAsync(string userId, string userToken)
	{
		var body = JsonSerializer.Serialize(new EntityNetGameRequest
		{
			Version = "2.12",
			ChannelId = 5
		});
		var httpResponseMessage = await _client.PostAsync("/pe-game/query/get-list-v4", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entities.G79.Entities<EntityNetGame>>(text);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00004B78 File Offset: 0x00002D78
	public Entity<EntityNetGameServerAddress> GetNetGameServerAddress(string userId, string userToken, string gameId)
	{
		return GetNetGameServerAddressAsync(userId, userToken, gameId).GetAwaiter().GetResult();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00004BA0 File Offset: 0x00002DA0
	private async Task<Entity<EntityNetGameServerAddress>> GetNetGameServerAddressAsync(string userId, string userToken, string gameId)
	{
		var body = JsonSerializer.Serialize(new EntityNetGameServerAddressRequest
		{
			ItemId = gameId
		});
		var httpResponseMessage = await _client.PostAsync("/pe-game/query/get-server-address", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityNetGameServerAddress>>(text);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00004BFC File Offset: 0x00002DFC
	public string GetAvailableRentalGames(string userId, string userToken, int offset)
	{
		return GetAvailableRentalGamesAsync(userId, userToken, offset).GetAwaiter().GetResult();
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00004C24 File Offset: 0x00002E24
	private async Task<string> GetAvailableRentalGamesAsync(string userId, string userToken, int offset)
	{
		var body = JsonSerializer.Serialize(new EntityRentalGameRequest
		{
			SortType = 0,
			OrderType = 0,
			Offset = offset
		});
		var httpResponseMessage = await _client.PostAsync("/rental-server/query/available-by-sort-type", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return text;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00004C80 File Offset: 0x00002E80
	public Entity<EntityRentalGameServerAddress> GetRentalGameServerAddress(string userId, string userToken, string gameId, string password = "")
	{
		return GetRentalGameServerAddressAsync(userId, userToken, gameId, password).GetAwaiter().GetResult();
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00004CAC File Offset: 0x00002EAC
	private async Task<Entity<EntityRentalGameServerAddress>> GetRentalGameServerAddressAsync(string userId, string userToken, string gameId, string password = "")
	{
		var body = JsonSerializer.Serialize(new EntityRentalGameServerAddressRequest
		{
			ServerId = gameId,
			Password = password
		});
		var httpResponseMessage = await _client.PostAsync("/rental-server-world-enter/get", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntityRentalGameServerAddress>>(text);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00004D10 File Offset: 0x00002F10
	public Entity<EntitySetNickName> SetNickName(string userId, string userToken, string nickName)
	{
		return SetNickNameAsync(userId, userToken, nickName).GetAwaiter().GetResult();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00004D38 File Offset: 0x00002F38
	private async Task<Entity<EntitySetNickName>> SetNickNameAsync(string userId, string userToken, string nickName)
	{
		var body = JsonSerializer.Serialize(new EntitySetNickNameRequest
		{
			Name = nickName,
			EntityId = userId
		});
		var httpResponseMessage = await _client.PostAsync("/nickname-setting", body, delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, userId, userToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<Entity<EntitySetNickName>>(text);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00004D94 File Offset: 0x00002F94
	public G79()
	{
		const string text = "https://g79mclobt.minecraft.cn";
		var httpClientHandler = new HttpClientHandler();
		httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip;
		httpClientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
		httpClientHandler.UseProxy = false;
		_client = new HttpWrapper(text, null, httpClientHandler);
		_core = new HttpWrapper("https://g79obtcore.nie.netease.com:8443", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.UserAgent("okhttp/3.12.12");
		}, new HttpClientHandler
		{
			AutomaticDecompression = DecompressionMethods.GZip
		}, new Version(2, 0));
		_mgbSdk = new MgbSdk("x19");
	}

	// Token: 0x04000056 RID: 86
	private readonly HttpWrapper _client;

	// Token: 0x04000057 RID: 87
	private readonly HttpWrapper _core;

	// Token: 0x04000058 RID: 88
	private readonly MgbSdk _mgbSdk;
}