using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Context;

public class AuthDbContext : IdentityDbContext<IdentityUser>
{

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
