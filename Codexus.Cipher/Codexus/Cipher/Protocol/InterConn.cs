using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.InterConn;
using Codexus.Cipher.Utils.Cipher;
using Codexus.Cipher.Utils.Http;
using Serilog;

namespace Codexus.Cipher.Protocol;

// Token: 0x02000021 RID: 33
public static class InterConn
{
	// Token: 0x060000C3 RID: 195 RVA: 0x00004E48 File Offset: 0x00003048
	public static async Task LoginStart(string entityId, string entityToken)
	{
		var httpResponseMessage = await Core.PostAsync("/interconn/web/game-play-v2/login-start", "{\"strict_mode\":true}", delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, entityId, entityToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		Log.Debug("LoginStart response: {0}", text);
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00004E94 File Offset: 0x00003094
	public static async Task GameStart(string entityId, string entityToken, string gameId)
	{
		var httpResponseMessage = await Core.PostAsync("/interconn/web/game-play-v2/start", JsonSerializer.Serialize(new InterConnGameStart
		{
			GameId = gameId,
			ItemList = ["10000"]
		}), delegate(HttpWrapper.HttpWrapperBuilder builder)
		{
			builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, entityId, entityToken));
		});
		var text = await httpResponseMessage.Content.ReadAsStringAsync();
		Log.Debug("GameStart response: {0}", text);
	}

	// Token: 0x04000059 RID: 89
	private static readonly HttpWrapper Core = new("https://x19obtcore.nie.netease.com:8443", delegate(HttpWrapper.HttpWrapperBuilder builder)
	{
		builder.UserAgent(WPFLauncher.GetUserAgent());
	});
}