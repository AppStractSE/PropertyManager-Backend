namespace Domain.Domain.Authentication;

public class TokenInfo
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}