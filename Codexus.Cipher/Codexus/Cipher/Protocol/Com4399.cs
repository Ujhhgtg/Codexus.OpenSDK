using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.Com4399;
using Codexus.Cipher.Extensions.Com4399Extensions;
using Codexus.Cipher.Utils.Http;
using Codexus.Development.SDK.Utils;
using Serilog;

namespace Codexus.Cipher.Protocol;

public class Com4399
{
    public Com4399()
    {
        CreateOrLoadDeviceAsync().GetAwaiter().GetResult();
    }

    private async Task CreateOrLoadDeviceAsync()
    {
        var savedData = MPay.LoadFromFile("4399com.cds");
        var device = savedData != null ? LoadDevice(savedData) : await CreateDeviceAsync();

        if (device.DeviceState == null) device = await CreateDeviceAsync();

        _deviceIdentifier = device.DeviceIdentifier;
        _deviceIdentifierSm = device.DeviceIdentifierSm;
        _udid = device.DeviceUdid;
        _state = device.DeviceState;
    }

    private static Entity4399Device LoadDevice(byte[] data)
    {
        return JsonSerializer.Deserialize<Entity4399Device>(data)!;
    }

    private async Task<Entity4399Device> CreateDeviceAsync()
    {
        _deviceIdentifier = GenerateIdentifier();
        _deviceIdentifierSm = GenerateIdentifier();
        _udid = Guid.NewGuid().ToString();
        var entity4399Device = new Entity4399Device
        {
            DeviceIdentifier = _deviceIdentifier,
            DeviceIdentifierSm = _deviceIdentifierSm,
            DeviceUdid = _udid,
            DeviceState = await OAuthDevice()
        };
        using (_lock.EnterScope())
        {
            MPay.SaveToFile("4399com.cds", JsonSerializer.Serialize(entity4399Device, DefaultOptions));
            return entity4399Device;
        }
    }

    private async Task<string> OAuthDevice()
    {
        var body = new ParameterBuilder().Append("usernames", "").Append("top_bar", "1").Append("state", "")
            .Append("device", JsonSerializer.Serialize(new Entity4399OAuth
            {
                DeviceIdentifier = _deviceIdentifier,
                DeviceIdentifierSm = _deviceIdentifierSm,
                Udid = _udid
            }, DefaultOptions))
            .FormUrlEncode();
        var httpResponseMessage =
            await _4399Api.PostAsync("/openapiv2/oauth.html", body, "application/x-www-form-urlencoded");
        httpResponseMessage.EnsureSuccessStatusCode();
        var text = await httpResponseMessage.Content.ReadAsStringAsync();
        var entity4399OAuthResponse = JsonSerializer.Deserialize<Entity4399OAuthResponse>(text);
        return entity4399OAuthResponse == null
            ? throw new Exception("Failed to deserialize: " + httpResponseMessage.Content.ReadAsStringAsync().Result)
            : new ParameterBuilder(entity4399OAuthResponse.Result.LoginUrl).Get("state");
    }

    public async Task<string> LoginAndAuthorize(string username, string password, string? captcha = null, string? captchaId = null, int retry = 0)
    {
        while (true)
        {
            if (retry > 5) throw new Exception("Retry limit exceeded");
            var parameterBuilder = new ParameterBuilder().Append("_d", _deviceIdentifier)
                .Append("access_token", "")
                .Append("aid", "")
                .Append("auth_action", "ORILOGIN")
                .Append("auto_scroll", "")
                .Append("autoCreateAccount", "")
                .Append("bizId", "2100001792")
                .Append("cid", "")
                .Append("client_id", "40f9e9b95d6c71ba5c6e0bd14c0abeff")
                .Append("css", "")
                .Append("expand_ext_login_list", "")
                .Append("game_key", "115716")
                .Append("isInputRealname", "false")
                .Append("isValidRealname", "false")
                .Append("password", password)
                .Append("redirect_uri", "https://m.4399api.com/openapi/oauth-callback.html?gamekey=44770")
                .Append("ref", "{\"game\":\"115716\",\"channel\":\"\"}")
                .Append("reg_mode", "reg_phone")
                .Append("response_type", "TOKEN")
                .Append("scope", "basic")
                .Append("sec", "1")
                .Append("show_4399", "")
                .Append("show_back_button", "")
                .Append("show_close_button", "")
                .Append("show_ext_login", "")
                .Append("show_forget_password", "")
                .Append("show_topbar", "false")
                .Append("state", _state)
                .Append("uid", "")
                .Append("username_history", "")
                .Append("username", username.ToLowerInvariant());
            if (captcha != null && captchaId != null) parameterBuilder.Append("captcha", captcha).Append("captcha_id", captchaId);
            var body = parameterBuilder.FormUrlEncode();
            Log.Information("Executing loginAndAuthorize...");
            var httpResponseMessage = await _login.PostAsync("/oauth2/loginAndAuthorize.do?channel=", body, "application/x-www-form-urlencoded");
            var text4 = await httpResponseMessage.Content.ReadAsStringAsync();
            if (text4.Contains("验证码"))
            {
                var text5 = await HandleCaptchaWithHtml(username, password, text4, retry);
                return text5;
            }

            var parameterBuilder2 = new ParameterBuilder(httpResponseMessage.Headers.Location.AbsoluteUri);
            if (captcha != null && captchaId == null) parameterBuilder2.Append("captcha", captcha);
            var httpResponseMessage2 = await new HttpWrapper(parameterBuilder2.ToQueryUrl()).GetAsync("");
            httpResponseMessage2.EnsureSuccessStatusCode();
            var text7 = await httpResponseMessage2.Content.ReadAsStringAsync();
            if (text7.Contains("登录状态已失效，请重新登录"))
            {
                var text8 = await OAuthDevice();
                var value = new Entity4399Device { DeviceIdentifier = _deviceIdentifier, DeviceIdentifierSm = _deviceIdentifierSm, DeviceUdid = _udid, DeviceState = text8 };
                _state = text8;
                using (_lock.EnterScope())
                {
                    MPay.SaveToFile("4399com.cds", JsonSerializer.Serialize(value, DefaultOptions));
                }

                captcha = null;
                captchaId = null;
                retry = 0;
                continue;
            }

            if (text7.Contains("登录成功，但账号存在异常，需要验证"))
            {
                var text12 = await CaptchaHandler.HandleLoginCaptchaAsync(text7);
                var text13 = await LoginAndAuthorize(username, password, text12, null, retry + 1);
                return text13;
            }

            var entity4399UserInfoResponse = JsonSerializer.Deserialize<Entity4399UserInfoResponse>(text7);
            if (entity4399UserInfoResponse is not { Code: "100" }) throw new Exception("Failed to deserialize: " + text7);
            var result = entity4399UserInfoResponse.Result;
            return _mgbSdk.GenerateSAuth(Guid.NewGuid().ToString("N").ToUpper(), "", result.Uid.ToString(), result.State, "", "4399com", "ad");
        }
    }

    private async Task<string> HandleCaptchaWithHtml(string username, string password, string html, int retry)
    {
        var match = CaptchaRegex().Match(html);
        var flag = !match.Success;
        if (flag) throw new Exception("Cannot find captcha in html");
        var matchedCaptchaId = match.Groups[1].Value;
        var httpResponseMessage = await _login.GetAsync("/ptlogin/captcha.do?captchaId=" + matchedCaptchaId + "&xx=1");
        var array = await httpResponseMessage.Content.ReadAsByteArrayAsync();
        var captcha = _nexus.ComputeCaptchaAsync(array);
        return await LoginAndAuthorize(username, password, captcha, matchedCaptchaId, retry + 1);
    }

    private static string GenerateIdentifier(DateTime? dateTime = null, string? additionalData = null)
    {
        var text = (dateTime ?? DateTime.Now).ToString("yyyyMMddHHmm");
        var text2 = GenerateHash50(additionalData);
        return text + text2;
    }

    private static string GenerateHash50(string? data = null)
    {
        if (string.IsNullOrEmpty(data)) data = Guid.NewGuid().ToString() + DateTime.Now.Ticks;
        return Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(data))).Substring(0, 50);
    }

    private static extern Regex CaptchaRegex();

    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly HttpWrapper _4399Api = new("https://m.4399api.com");
    private readonly Lock _lock = new();

    private readonly HttpWrapper _login = new("https://ptlogin.4399.com", null, new HttpClientHandler
    {
        AllowAutoRedirect = false
    });

    private readonly MgbSdk _mgbSdk = new("x19");
    private readonly WebNexusApi _nexus = new("YXBwSWQ9Q29kZXh1cy5HYXRld2F5LmFwcFNlY3JldD1hN0s5bTJYcUw4YkM0d1ox");
    private string _deviceIdentifier = string.Empty;
    private string _deviceIdentifierSm = string.Empty;
    private string _state = string.Empty;
    private string _udid = string.Empty;
}
