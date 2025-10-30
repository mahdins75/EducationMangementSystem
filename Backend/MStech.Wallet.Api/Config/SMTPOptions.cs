namespace MStech.Wallet.Api.Config
{
    public class SmtpOption
    {
        public string HostUrl { get; set; }
        public int HostPort { get; set; }
        public bool EnableSsl { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }
}
