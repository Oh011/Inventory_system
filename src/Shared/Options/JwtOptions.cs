namespace Sahred.Options
{
    public class JwtOptions
    {

        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationInHours { get; set; }

    }
}
