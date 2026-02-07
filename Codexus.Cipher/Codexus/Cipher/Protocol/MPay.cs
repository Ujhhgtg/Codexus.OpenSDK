using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Codexus.Cipher.Entities.MPay;
using Codexus.Cipher.Extensions;
using Codexus.Cipher.Utils;
using Codexus.Cipher.Utils.Exception;
using Codexus.Cipher.Utils.Http;
using Serilog;

namespace Codexus.Cipher.Protocol;

public class MPay : IDisposable
{
    private string GameId { get; }
    public string GameVersion { get; }

    public MPay(string gameId, string gameVersion)
    {
        GameId = gameId;
        GameVersion = gameVersion;
        Unique = CreateOrLoadUnique(gameId);
        _device = CreateOrLoadDeviceAsync(gameId, gameVersion).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        _client.Dispose();
        _service.Dispose();
        GC.SuppressFinalize(this);
    }

    public EntityDevice GetDevice()
    {
        return _device;
    }

    public async Task Configure(string appKey, string channel)
    {
        var text = new ParameterBuilder().Append("sub_app_key", "").Append("api_ver", "2").Append("gdpr", "0")
            .Append("app_channel", channel)
            .Append("sdk_version", "c1.0.0")
            .Append("app_key", appKey)
            .Append("device_id", Unique.ToUpper())
            .FormUrlEncode();
        var httpResponseMessage = await _client.GetAsync("https://analytics.mpay.netease.com/config?" + text);
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    private static string CreateOrLoadUnique(string gameId)
    {
        var text = gameId + "-guid.cds";
        var array = LoadFromFile(text);
        var flag = array != null;
        var text2 = flag ? Encoding.UTF8.GetString(array) : CreateUnique(text);
        return text2;
    }

    private static string CreateUnique(string fileName)
    {
        var text = Guid.NewGuid().ToString().Replace("-", "");
        SaveToFile(fileName, text);
        return text;
    }

    private async Task<EntityDevice> CreateOrLoadDeviceAsync(string gameId, string gameVersion)
    {
        var text = gameId + "-device.cds";
        var array = LoadFromFile(text);
        var flag = array == null;
        EntityDevice entityDevice2;
        if (flag)
        {
            var entityDevice = await CreateDeviceAsync(gameId, gameVersion, text);
            entityDevice2 = entityDevice;
        }
        else
        {
            entityDevice2 = JsonSerializer.Deserialize<EntityDeviceResponse>(Encoding.UTF8.GetString(array))
                .EntityDevice;
        }

        return entityDevice2;
    }

    private async Task<EntityDevice> CreateDeviceAsync(string gameId, string gameVersion, string fileName)
    {
        var httpResponseMessage = await _service.PostAsync("/mpay/games/" + gameId + "/devices",
            BuildDeviceParams().Append("unique_id", Unique).FormUrlEncode(), "application/x-www-form-urlencoded");
        httpResponseMessage.EnsureSuccessStatusCode();
        var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
        SaveToFile(fileName, text2);
        return JsonSerializer.Deserialize<EntityDeviceResponse>(text2).EntityDevice;
    }

    public async Task<EntityMPayUserResponse> LoginWithEmailAsync(string email, string password)
    {
        var value = JsonSerializer.Serialize(new EntityUsersParameters
        {
            Password = password.EncodeMd5(),
            Unique = Unique,
            Username = email
        }, DefaultOptions).EncodeAes(_device.Key.DecodeHex()).EncodeHex();
        var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
        defaultInterpolatedStringHandler.AppendLiteral("/mpay/games/");
        defaultInterpolatedStringHandler.AppendFormatted(GameId);
        defaultInterpolatedStringHandler.AppendLiteral("/devices/");
        defaultInterpolatedStringHandler.AppendFormatted(_device.Id);
        defaultInterpolatedStringHandler.AppendLiteral("/users");
        var httpResponseMessage = await _service.PostAsync(defaultInterpolatedStringHandler.ToStringAndClear(),
            BuildBaseParams()
                .Append("opt_fields",
                    "nickname,avatar,realname_status,mobile_bind_status,mask_related_mobile,related_login_status")
                .Append("params", value)
                .Append("un", email.EncodeBase64())
                .FormUrlEncode(), "application/x-www-form-urlencoded");
        var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
        if (httpResponseMessage.IsSuccessStatusCode) return JsonSerializer.Deserialize<EntityMPayUserResponse>(text2);
        var entityVerifyResponse = JsonSerializer.Deserialize<EntityVerifyResponse>(text2);
        if (entityVerifyResponse is { Code: 1351 }) throw new VerifyException(text2);
        throw new Exception("Failed to login with email, response: " + text2);
    }

    public async Task<bool> SendSmsCodeAsync(string phoneNumber)
    {
        var httpResponseMessage = await _service.PostAsync("/mpay/api/users/login/mobile/get_sms", BuildBaseParams()
            .Append("device_id", _device.Id).Append("mobile", phoneNumber)
            .FormUrlEncode(), "application/x-www-form-urlencoded");
        var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode) Log.Error("Failed to send sms code, response: {Json}", text2);
        return httpResponseMessage.IsSuccessStatusCode;
    }

    public async Task<EntitySmsTicket> VerifySmsCodeAsync(string phoneNumber, string code)
    {
        var httpResponseMessage = await _service.PostAsync("/mpay/api/users/login/mobile/verify_sms", BuildBaseParams()
            .Append("device_id", _device.Id).Append("mobile", phoneNumber)
            .Append("smscode", code)
            .Append("up_content", "")
            .FormUrlEncode(), "application/x-www-form-urlencoded");
        var text2 = await httpResponseMessage.Content.ReadAsStringAsync();
        EntitySmsTicket entitySmsTicket;
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            entitySmsTicket = JsonSerializer.Deserialize<EntitySmsTicket>(text2);
        }
        else
        {
            Log.Error("Failed to send sms code, response: {Json}", text2);
            entitySmsTicket = null;
        }

        return entitySmsTicket;
    }

    public async Task<EntityMPayUserResponse> FinishSmsCodeAsync(string phoneNumber, string ticket)
    {
        var text = phoneNumber.EncodeBase64();
        var httpResponseMessage = await _service.PostAsync("/mpay/api/users/login/mobile/finish?un=" + text,
            BuildBaseParams().Append("device_id", _device.Id).Append("opt_fields",
                    "nickname,avatar,realname_status,mobile_bind_status,mask_related_mobile,related_login_status")
                .Append("ticket", ticket)
                .FormUrlEncode(), "application/x-www-form-urlencoded");
        var text3 = await httpResponseMessage.Content.ReadAsStringAsync();
        EntityMPayUserResponse entityMPayUserResponse;
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            entityMPayUserResponse = JsonSerializer.Deserialize<EntityMPayUserResponse>(text3);
        }
        else
        {
            Log.Error("Failed to finish sms code, response: {Json}", text3);
            entityMPayUserResponse = null;
        }

        return entityMPayUserResponse;
    }

    private ParameterBuilder BuildBaseParams()
    {
        var parameterBuilder = new ParameterBuilder().Append("app_channel", "netease").Append("app_mode", "2")
            .Append("app_type", "games")
            .Append("arch", "win_x64")
            .Append("cv", "c4.2.0")
            .Append("mcount_app_key", "EEkEEXLymcNjM42yLY3Bn6AO15aGy4yq")
            .Append("mcount_transaction_id", "0");
        const string text = "process_id";
        var defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
        defaultInterpolatedStringHandler.AppendFormatted(Environment.ProcessId);
        return parameterBuilder.Append(text, defaultInterpolatedStringHandler.ToStringAndClear())
            .Append("sv", "10.0.22621").Append("updater_cv", "c1.0.0")
            .Append("game_id", GameId)
            .Append("gv", GameVersion);
    }

    private ParameterBuilder BuildDeviceParams()
    {
        return BuildBaseParams().Append("brand", "Microsoft").Append("device_model", "pc_mode")
            .Append("device_name", "PC-" + StringGenerator.GenerateRandomString(12))
            .Append("device_type", "Computer")
            .Append("init_urs_device", "0")
            .Append("mac", StringGenerator.GenerateRandomMacAddress())
            .Append("resolution", "1920x1080")
            .Append("system_name", "windows")
            .Append("system_version", "10.0.22621");
    }

    public static void SaveToFile(string filename, string content)
    {
        File.WriteAllText(filename, content);
    }

    public static byte[]? LoadFromFile(string filename)
    {
        byte[] array;
        try
        {
            var flag = !File.Exists(filename);
            if (flag)
            {
                array = null;
            }
            else
            {
                using var fileStream = new FileStream(filename, FileMode.Open);
                var array2 = new byte[fileStream.Length];
                fileStream.ReadExactly(array2, 0, array2.Length);
                array = array2;
            }
        }
        catch (Exception)
        {
            array = null;
        }

        return array;
    }

    private readonly EntityDevice _device;
    private readonly HttpWrapper _client = new();
    private readonly HttpWrapper _service = new("https://service.mkey.163.com");
    public readonly string Unique;

    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}