namespace Core.Domain.Authentication;

public class User
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Role { get; set; }
}