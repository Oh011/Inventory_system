namespace Shared.Options
{
    public class SmtpOptions
    {

        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string From { get; set; } = default!;
        public string DisplayName { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
