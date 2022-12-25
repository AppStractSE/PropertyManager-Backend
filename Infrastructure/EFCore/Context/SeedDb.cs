using Domain.Repository.Entities;
using Infrastructure.Context;

namespace Infrastructure.EFCore.Context;

public class SeedDb
{
    private static PropertyManagerContext _context;
    public static async Task SeedAsync(PropertyManagerContext context)
    {
        context.Database.EnsureCreated();
        _context = context;

        if (!_context.Customers.Any())
        {
            _context.Customers.AddRange(GetPreconfiguredCustomers());
            await _context.SaveChangesAsync();
        }

    }

    private static IEnumerable<Customer> GetPreconfiguredCustomers()
    {
        var list = new List<Customer>();

        for (int i = 0; i < 5; i++)
        {
            list.Add(
            new Customer
            {
                Id = Guid.NewGuid(),
                AreaId = "",
                TeamId = "",
                Name = "Test "+(i+1),
            });
        }
        return list;

    }
}