using RestSharp;
using Microsoft.Extensions.Configuration;

namespace Implementation.Notification
{
    public class ModirPayamakService
    {

        //private const string _registerPattern = "9amg1dzuh2";
        private const string _forgetPassPattern = "u0fd5otk3n";
        private const string _remainingPattern = "jztk7n2oi0";
        private const string _successPransaction = "ur5kw7yicc";
        private const string _fromNum_pattern = "3000505";
        private const string _reservationReminder = "4o6uqsglsg55mpg";
        private const string _reservationFinalReminder = "i2nw23fn99956hr";
        IConfigurationRoot MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly IConfiguration _config;

        //private const string _userName = "9183638330";
        //private const string _password = "Amir@#8330";

        //private const string _userName = MyConfig.GetValue<int>("AppSettings:SampleIntValue");
        //private const string _password = MyConfig.GetValue<int>("AppSettings:SampleIntValue");

        private string _userName = "";
        private string _password = "";

        public ModirPayamakService(IConfiguration iconfig)
        {
            _config = iconfig;
            _userName = _config.GetValue<string>("MODIR_PAYAMAK_USERNAME");
            _password = _config.GetValue<string>("MODIR_PAYAMAK_PASSWORD");

        }

        //public string SendRegisterCode(int registerCode, string toNumber)
        //    => Send(toNumber, $"{{\"register-code\": \"{registerCode}\"}}", _registerPattern);

        public string SendForgetPassCode(int code, string toNumber)
            => Send(toNumber, $"{{\"code\": \"{code}\"}}", _forgetPassPattern);

        public string SendSuccessTransaction(string toNumber, int count, string tour, string agency)
            => Send(toNumber, $"{{\"tour\": \"{tour}\", \"count\": \"{count}\", \"agency\": \"{agency}\"}}", _successPransaction);

        public string SendRemaining(string toNumber, string tourTitle, int count)
            => Send(toNumber, $"{{\"tour\": \"{tourTitle}\", \"count\": \"{count}\"}}", _remainingPattern);

        public string SendRegisterConfirmation(string toNumber, string code)
                    => Send(toNumber, $"{{\"tour\": \"{code}\", \"count\": \"\"}}", _remainingPattern);
        public string SendReservationReminder(string name, string toNumber, string tour, string agency, string link)
         => Send(toNumber, $"{{\"name\": \"{name}\", \"tour\": \"{tour}\", \"agency\": \"{agency}\", \"link\": \"{link}\"}}", _reservationReminder);

        public string SendReservationFinalReminder(string name, string toNumber, string tour, string agency, string link)
         => Send(toNumber, $"{{\"name\": \"{name}\", \"tour\": \"{tour}\", \"agency\": \"{agency}\", \"link\": \"{link}\"}}", _reservationFinalReminder);

        string Send(string toNumber, string inputData, string pattern)
        {
            var jsonData = $@"{{
    ""code"": ""{pattern}"",
    ""sender"": ""+983000505"",
    ""recipient"": ""{toNumber}"",
    ""variable"": 
        {inputData}
    
}}";
            var client = new RestClient("https://api2.ippanel.com/api/v1/");
            var request = new RestRequest("sms/pattern/normal/send", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("apikey", "s8WHTL4J9J2dlEc1UwYJD4wI_-nRIXvvqvHw2iesMqM=");

            request.AddParameter("application/json", jsonData, ParameterType.RequestBody);


            var response = client.Execute(request);
            return response.Content;

        }
    }
}
