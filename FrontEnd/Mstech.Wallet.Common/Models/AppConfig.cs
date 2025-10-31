namespace Common.Models
{
    public class AppConfig
    {
        public SmsOption SmsOptions { get; set; }
        public SmtpOption SmtpOption { get; set; }
    }

    public class SmsOption
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SenderNumber { get; set; }
    }

    public class SmtpOption
    {
        public string HostUrl { get; set; }
        public int HostPort { get; set; }
        public bool EnableSsl { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }
}
