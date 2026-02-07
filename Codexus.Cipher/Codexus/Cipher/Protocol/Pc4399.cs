using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.Pc4399;
using Codexus.Cipher.Utils;
using Codexus.Cipher.Utils.Exception;
using Codexus.Cipher.Utils.Http;

namespace Codexus.Cipher.Protocol;

public class Pc4399 : IDisposable
{
    public MPay MPay { get; }

    public Pc4399()
    {
        MPay = new MPay("aecfrxodyqaaaajp-g-x19", WPFLauncher.GetLatestVersionAsync().Result);
        _login = new HttpWrapper("https://ptlogin.4399.com", null, new HttpClientHandler
        {
            CookieContainer = _cookieContainer,
            UseCookies = true,
            AllowAutoRedirect = true
        });
    }

    public void Dispose()
    {
        MPay.Dispose();
        _mgbSdk.Dispose();
        _login.Dispose();
        _cookieContainer.GetAllCookies().Clear();
        GC.SuppressFinalize(this);
    }

    public async Task<string> LoginWithPasswordAsync(string username, string password, string? captchaIdentifier,
        string? captcha)
    {
        var parameter = BuildParametersLogin().Append("username", username).Append("password", password);
        var flag2 = captchaIdentifier == null && captcha == null;
        if (flag2)
        {
            var httpResponseMessage = await _login.PostAsync("/ptlogin/loginFrame.do?v=1", parameter.FormUrlEncode(),
                "application/x-www-form-urlencoded");
            var text = await httpResponseMessage.Content.ReadAsStringAsync();
            flag2 = text.Contains("账号异常，请输入验证码");
        }

        if (flag2) throw new CaptchaException("^Captcha required^");
        if (captchaIdentifier != null && captcha != null)
            parameter.Append("sessionId", captchaIdentifier).Append("inputCaptcha", captcha);
        var httpResponseMessage2 = await _login.PostAsync("/ptlogin/login.do?v=1", parameter.FormUrlEncode(),
            "application/x-www-form-urlencoded");
        if (!httpResponseMessage2.IsSuccessStatusCode)
        {
            throw new Exception("Login to Pc499 failed");
        }

        return await GenerateCookie();
    }

    private async Task<string> GenerateCookie()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var @params = new ParameterBuilder()
            .Append("appId", "kid_wdsj")
            .Append("gameUrl", "https://cdn.h5wan.4399sj.com/microterminal-h5-frame?game_id=500352")
            .Append("isCrossDomain", "1")
            .Append("nick", "null")
            .Append("onLineStart", "false")
            .Append("ptLogin", "true")
            .Append("rand_time", "$randTime");
        var httpResponseMessage2 = await _login.GetAsync("/ptlogin/checkKidLoginUserCookie.do?" + @params
            .Append("retUrl",
                $"https://ptlogin.4399.com/resource/ucenter.html?action=login&appId=kid_wdsj&loginLevel=8&regLevel=8&bizId=2201001794&externalLogin=qq&qrLogin=true&layout=vertical&level=101&css=https://microgame.5054399.net/v2/resource/cssSdk/default/login.css&v=2018_11_26_16&postLoginHandler=redirect&checkLoginUserCookie=true&redirectUrl=http%3A%2F%2Fcdn.h5wan.4399sj.com%2Fmicroterminal-h5-frame%3Fgame_id%3D500352%26rand_time%3D{timestamp}"
                )
            .Append("show", "1").FormUrlEncode());
        if (httpResponseMessage2.RequestMessage == null || httpResponseMessage2.RequestMessage.RequestUri == null)
            throw new Exception("Login to Pc499 failed");
        string text;
        var num = (text = httpResponseMessage2.RequestMessage.RequestUri.ToString()).LastIndexOf('?') + 1;
        var parameter = text[num..];
        var parameterBuilder3 = await GetUniAuth(parameter);
        return _mgbSdk.GenerateSAuth(MPay.Unique, parameterBuilder3.Get("username"), parameterBuilder3.Get("uid"),
            parameterBuilder3.Get("token"), parameterBuilder3.Get("time"), "4399pc");
    }

    private async Task<ParameterBuilder> GetUniAuth(string parameter)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var callback = $"jQuery1830{StringGenerator.GenerateRandomString(16, true, false, false)}_{timestamp}";
        var httpResponseMessage = await _service.GetAsync("/v2/service/sdk/info?" + new ParameterBuilder()
            .Append("callback", callback).Append("queryStr", parameter).FormUrlEncode());
        if (!httpResponseMessage.IsSuccessStatusCode) throw new Exception("Get Uni-auth failed");
        var num = (await httpResponseMessage.Content.ReadAsStringAsync()).IndexOf(callback + "(", StringComparison.Ordinal) + callback.Length + 1;
        var entity4399Response =
            JsonSerializer.Deserialize<Entity4399Response>((await httpResponseMessage.Content.ReadAsStringAsync()).Substring(num, (await httpResponseMessage.Content.ReadAsStringAsync()).Length - 1 - num));
        return entity4399Response == null
            ? throw new Exception("Get Uni-auth failed")
            : new ParameterBuilder(entity4399Response.Data.SdkLoginData);
    }

    private static ParameterBuilder BuildParametersLogin()
    {
        return new ParameterBuilder().Append("appId", "kid_wdsj").Append("autoLogin", "on")
            .Append("bizId", "2201001794")
            .Append("css", "https://microgame.5054399.net/v2/resource/cssSdk/default/login.css")
            .Append("displayMode", "popup")
            .Append("externalLogin", "qq")
            .Append("gameId", "wd")
            .Append("iframeId", "popup_login_frame")
            .Append("includeFcmInfo", "false")
            .Append("layout", "vertical")
            .Append("layoutSelfAdapting", "true")
            .Append("level", "8")
            .Append("loginFrom", "uframe")
            .Append("mainDivId", "popup_login_div")
            .Append("postLoginHandler", "default")
            .Append("redirectUrl", "")
            .Append("regLevel", "8")
            .Append("sec", "1")
            .Append("sessionId", "")
            .Append("userNameLabel", "4399用户名")
            .Append("userNameTip", "请输入4399用户名")
            .Append("welcomeTip", "欢迎回到4399");
    }

    private readonly CookieContainer _cookieContainer = new();
    private readonly HttpWrapper _login;
    private readonly MgbSdk _mgbSdk = new("x19");
    private readonly HttpWrapper _service = new("https://microgame.5054399.net");
}