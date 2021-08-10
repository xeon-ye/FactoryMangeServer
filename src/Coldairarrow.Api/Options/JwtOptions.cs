namespace Coldairarrow.Api
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string User { get; set; }
        public int AccessExpireHours { get; set; }
        public int RefreshExpireHours { get; set; }
    }
}

