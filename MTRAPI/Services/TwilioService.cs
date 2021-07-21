
using System;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MTR.Services
{
    public class TwilioService:ISMSService
    { 
        private readonly IConfiguration _configuration;
        private readonly string _accountSid ;
        private readonly string _authToken ;
        private readonly string _FromPhoneNumber;

        public TwilioService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSid = _configuration["TwilioAccountID"];
            _authToken = _configuration["TwilioAuthToken"];
            _FromPhoneNumber = _configuration["TwilioFromPhoneNumber"];

            TwilioClient.Init(_accountSid, _authToken);
        }


        public void SendSMS(string message, string ToPhoneNumber)
        {
            ToPhoneNumber = PrefixPlusTwoFiveFour(ToPhoneNumber);
            var TwilioMessage = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(_FromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(ToPhoneNumber)
            );

        }

        private string PrefixPlusTwoFiveFour(string phoneNumber)
        {
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = "+254" + phoneNumber.Substring(1);
            }
            if (phoneNumber.StartsWith("+") == false)
            {
                phoneNumber = "+" + phoneNumber;
            }
            return phoneNumber;
        }


    }

}
