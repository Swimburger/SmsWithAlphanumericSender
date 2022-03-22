# Send SMS with an Alphanumeric Sender using Twilio Programmable SMS

To run this sample:
1. Configure the project using user-secrets
```bash
dotnet user-secrets set TwilioAccountSid <YourAccountSid>
dotnet user-secrets set TwilioAuthToken <YourAuthToken>

# if you want to use a Messaging Service:
dotnet user-secrets set TwilioMessagingServiceSid <YourMessagingServiceSid>

# if you don't want to use a Messaging Service:
dotnet user-secrets set AlphanumericSender <YourAlphanumericSender>
```

2. Run the project with the following arguments:
```bash
dotnet run --environment=Development --UseMessagingService=<Replace with True/False>
```