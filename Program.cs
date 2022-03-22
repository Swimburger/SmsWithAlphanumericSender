using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

// build configuration
using var host = Host.CreateDefaultBuilder(args).Build();
var config = host.Services.GetRequiredService<IConfiguration>();

// grab configuration
var useMessagingService = config.GetValue<bool>("UseMessagingService");
var alphanumericSender = config["AlphanumericSender"];
var messagingServiceSid = config["TwilioMessagingServiceSid"];
var accountSid = config["TwilioAccountSid"];
var authToken = config["TwilioAuthToken"];

// ask user for phone number and message to text
Console.Write("Who do you want to text? ");
var toPhoneNumber = Console.ReadLine();
Console.WriteLine();

Console.Write("What is your message? ");
var messageBody = Console.ReadLine();
Console.WriteLine();

// authenticate using the TwilioClient
TwilioClient.Init(accountSid, authToken);

if (useMessagingService)
{
    // send text message using a Messaging Service
    MessageResource.Create(
        to: new PhoneNumber(toPhoneNumber),
        messagingServiceSid: messagingServiceSid,
        body: messageBody
    );
}
else
{
    // send text message without a Messaging Service
    var request = new Request(Twilio.Http.HttpMethod.Post, $"https://api.twilio.com/2010-04-01/Accounts/{accountSid}/Messages.json");
    request.AddPostParam("Body",messageBody);
    request.AddPostParam("To",toPhoneNumber);
    request.AddPostParam("From",alphanumericSender);
    TwilioClient.GetRestClient().Request(request);
}
