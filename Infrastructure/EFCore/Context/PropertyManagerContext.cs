using Domain.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class PropertyManagerContext : DbContext
{

    public PropertyManagerContext(DbContextOptions<PropertyManagerContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasKey(x => x.Id);
    }
}