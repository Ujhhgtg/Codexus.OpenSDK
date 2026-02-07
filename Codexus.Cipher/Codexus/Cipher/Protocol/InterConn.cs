using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.InterConn;
using Codexus.Cipher.Utils.Cipher;
using Codexus.Cipher.Utils.Http;
using Serilog;

namespace Codexus.Cipher.Protocol;

public static class InterConn
{
    public static async Task LoginStart(string entityId, string entityToken)
    {
        var httpResponseMessage = await Core.PostAsync("/interconn/web/game-play-v2/login-start",
            "{\"strict_mode\":true}",
            builder =>
            {
                builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, entityId, entityToken));
            });
        var text = await httpResponseMessage.Content.ReadAsStringAsync();
        Log.Debug("LoginStart response: {0}", text);
    }

    public static async Task GameStart(string entityId, string entityToken, string gameId)
    {
        var httpResponseMessage = await Core.PostAsync("/interconn/web/game-play-v2/start", JsonSerializer.Serialize(
                new InterConnGameStart
                {
                    GameId = gameId,
                    ItemList = ["10000"]
                }),
            builder =>
            {
                builder.AddHeader(TokenUtil.ComputeHttpRequestToken(builder.Url, builder.Body, entityId, entityToken));
            });
        var text = await httpResponseMessage.Content.ReadAsStringAsync();
        Log.Debug("GameStart response: {0}", text);
    }

    private static readonly HttpWrapper Core = new("https://x19obtcore.nie.netease.com:8443",
        builder => { builder.UserAgent(WPFLauncher.GetUserAgent()); });
}