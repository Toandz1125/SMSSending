using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

LoadEnvFile(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env"));

try
{
    TwilioClient.Init(
        Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
        Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN"));

    var message = await MessageResource.CreateAsync(
        body: "Hello from Twilio Trial",
        from: new PhoneNumber("+16292842479"),
        to: new PhoneNumber("+18777804236"));

    Console.WriteLine(message.Sid);
    Console.WriteLine(message.Status);
}
catch (ApiException ex)
{
    Console.WriteLine($"Code: {ex.Code}");
    Console.WriteLine($"Message: {ex.Message}");
    Console.WriteLine($"MoreInfo: {ex.MoreInfo}");
    Console.WriteLine($"Status: {ex.Status}");
}

static void LoadEnvFile(string path)
{
    if (!File.Exists(path)) return;

    foreach (var line in File.ReadAllLines(path))
    {
        var trimmed = line.Trim();
        if (trimmed.Length == 0 || trimmed.StartsWith('#')) continue;

        var separatorIndex = trimmed.IndexOf('=');
        if (separatorIndex < 0) continue;

        var key = trimmed[..separatorIndex].Trim();
        var value = trimmed[(separatorIndex + 1)..].Trim().Trim('"');
        Environment.SetEnvironmentVariable(key, value);
    }
}
