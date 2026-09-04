using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
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
