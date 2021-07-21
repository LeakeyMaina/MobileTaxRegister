
using System;
using Microsoft.Extensions.Configuration;
using AfricasTalkingCS;

namespace MTR.Services
{
    public class AfricasTalking:ISMSService
    { 
        private readonly IConfiguration _configuration;
        private readonly string _username;
        private readonly string _apikey;
        private readonly string _FromPhoneNumber;
        private readonly AfricasTalkingGateway _gateway;

        public AfricasTalking(IConfiguration configuration)
        {
            _configuration = configuration;
            _username = _configuration["AfricasTalkingUsername"];
            _apikey = _configuration["AfricasTalkingAPIkey"];
            _FromPhoneNumber = _configuration["AfricasTalkingPhone"];
            _gateway = new AfricasTalkingGateway(_username, _apikey);
        }


        public void SendSMS(string message, string ToPhoneNumber)
        {
            try
            {
                var sms = _gateway.SendMessage(ToPhoneNumber, message,"KRA");
            }
            catch (AfricasTalkingGatewayException exception)
            {
                Console.WriteLine(exception);
            }

        }

    }
  
}
