namespace Domain.Domain.Authentication
{
    public class AuthUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string DisplayName { get; set; }
    }
}
