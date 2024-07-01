using Microsoft.Extensions.Options;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Infrastructure.Options;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TenetTest.Infrastructure.Services.SmsService;

public class SmsService : ISmsService
{
    private readonly TwilioSettings _twilioOptions;

    public SmsService(IOptions<TwilioSettings> twilioOptions)
    {
        _twilioOptions = twilioOptions.Value;
        TwilioClient.Init(_twilioOptions.AccountSid, _twilioOptions.AuthToken);
    }

    public async Task SendSmsAsync(string toPhoneNumber, string message)
    {
        var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
        {
            From = new PhoneNumber(_twilioOptions.FromPhoneNumber),
            Body = message
        };

        await MessageResource.CreateAsync(messageOptions);
    }
}