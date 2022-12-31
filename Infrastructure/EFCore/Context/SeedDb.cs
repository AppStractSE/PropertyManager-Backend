using Domain.Repository.Entities;
using Infrastructure.Context;

namespace Infrastructure.EFCore.Context;

public class SeedDb
{
    private static PropertyManagerContext? _context;
    public static async Task SeedAsync(PropertyManagerContext context)
    {
        context.Database.EnsureCreated();
        _context = context;

        if (!context.Areas.Any()) GenerateAreas(context);
        if (!context.Customers.Any()) GenerateCustomers(context);
        if (!context.Chores.Any()) GenerateChores(context);
        if (!context.CustomerChores.Any()) GenerateCustomerChores(context);
    }
    private static async void GenerateAreas(PropertyManagerContext context)
    {
        context.Areas.AddRange(
            new Area
            {
                Id = Guid.NewGuid(),
                Name = "Norrmalm",
            },

            new Area
            {
                Id = Guid.NewGuid(),
                Name = "Billingelund",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateCustomerChores(PropertyManagerContext context)
    {
        context.CustomerChores.AddRange(
            new CustomerChore
            {
                Id = Guid.NewGuid(),
                CustomerId = _context.Customers.First(x => x.Name == "BRF Motorn").Id.ToString(),
                ChoreId = _context.Chores.First(x => x.Title == "Vårluckring i rabatt").Id.ToString(),
                Frequency = 1,
                PeriodicId = Guid.NewGuid().ToString(),
            },

            new CustomerChore
            {
                Id = Guid.NewGuid(),
                CustomerId = _context.Customers.First(x => x.Name == "BRF Motorn").Id.ToString(),
                ChoreId = _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString(),
                Frequency = 3,
                PeriodicId = Guid.NewGuid().ToString(),
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateCustomers(PropertyManagerContext context)
    {
        context.Customers.AddRange(
            new Customer
            {
                Id = Guid.NewGuid(),
                AreaId = _context.Areas.First(x => x.Name == "Norrmalm").Id.ToString(),
                TeamId = "",
                Name = "BRF Motorn",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateChores(PropertyManagerContext context)
    {
        context.Chores.AddRange(
            new Chore
            {
                Id = Guid.NewGuid(),
                Title = "Vårluckring i rabatt",
                Description = "Luckra rabatterna",
                CategoryId = Guid.NewGuid().ToString(),
            },

            new Chore
            {
                Id = Guid.NewGuid(),
                Title = "Beskärning buskar",
                Description = "Beskär buskarna",
                CategoryId = Guid.NewGuid().ToString(),
            });

        await _context.SaveChangesAsync();
    }
}