using Microsoft.AspNetCore.Identity;

namespace Domain.Repository.Entities;

public class AuthUserEntity : IdentityUser
{
    public string DisplayName { get; set; }
}
