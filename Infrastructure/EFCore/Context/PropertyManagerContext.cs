using Core.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class PropertyManagerContext : DbContext
{
    public PropertyManagerContext(DbContextOptions<PropertyManagerContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    public DbSet<Area> Areas { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Chore> Chores { get; set; }
    public DbSet<CustomerChore> CustomerChores { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Periodic> Periodics { get; set; }
    public DbSet<ChoreComment> ChoreComments { get; set; }
    public DbSet<ChoreStatus> ChoreStatuses { get; set; }
    public DbSet<ArchivedChoreStatus> ArchivedChoreStatuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>().HasKey(x => x.Id);
        modelBuilder.Entity<Customer>().HasKey(x => x.Id);
        modelBuilder.Entity<Chore>().HasKey(x => x.Id);
        modelBuilder.Entity<CustomerChore>().HasKey(x => x.Id);
        modelBuilder.Entity<Team>().HasKey(x => x.Id);
        modelBuilder.Entity<TeamMember>().HasKey(x => new { x.TeamId, x.UserId });
        modelBuilder.Entity<Periodic>().HasKey(x => x.Id);
        modelBuilder.Entity<ChoreComment>().HasKey(x => x.Id);
        modelBuilder.Entity<ChoreStatus>().HasKey(x => x.Id);
        modelBuilder.Entity<ArchivedChoreStatus>().HasKey(x => x.Id);
        modelBuilder.Entity<Category>().HasKey(x => x.Id);
        modelBuilder.Entity<SubCategory>().HasKey(x => x.Id);
        modelBuilder.Entity<City>().HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}