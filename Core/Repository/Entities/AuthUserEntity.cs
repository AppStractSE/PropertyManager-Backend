using Microsoft.AspNetCore.Identity;

namespace Core.Repository.Entities;

public class AuthUserEntity : IdentityUser
{
    public string DisplayName { get; set; }
}
