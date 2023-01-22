using Domain.Repository.Entities;
using Infrastructure.Context;

namespace Infrastructure.EFCore.Context;

public class SeedDb
{
    private static PropertyManagerContext? _context;
    public static Task SeedAsync(PropertyManagerContext context)
    {
        context.Database.EnsureCreated();
        _context = context;

        if (!context.Users.Any()) GenerateUsers(context);
        if (!context.Periodics.Any()) GeneratePeriodics(context);
        if (!context.Areas.Any()) GenerateAreas(context);
        if (!context.Teams.Any()) GenerateTeams(context);
        if (!context.TeamMembers.Any()) GenerateTeamMembers(context);
        if (!context.Customers.Any()) GenerateCustomers(context);
        if (!context.Chores.Any()) GenerateChores(context);
        if (!context.CustomerChores.Any()) GenerateCustomerChores(context);
        if (!context.ChoreComments.Any()) GenerateChoreComments(context);
        if (!context.ChoreStatuses.Any()) GenerateChoreStatuses(context);
        return Task.CompletedTask;
    }

    private static async void GeneratePeriodics(PropertyManagerContext context)
    {
        context.Periodics.AddRange(
            new Periodic
            {
                Name = "Dagligen",
            },           
             new Periodic
            {
                Name = "Veckovis",
            },

            new Periodic
            {
                Name = "Månadsvis",
            },

            new Periodic
            {
                Name = "Årligen",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateAreas(PropertyManagerContext context)
    {
        context.Areas.AddRange(
            new Area
            {
                Name = "Norrmalm",
            },
            new Area
            {
                Name = "Käpplunda",
            },

            new Area
            {
                Name = "Billingelund",
            });

        await _context.SaveChangesAsync();
    }

    private static async void GenerateTeams(PropertyManagerContext context)
    {
        context.Teams.AddRange(
            new Team
            {
                Name = "Team 1",
            },
            new Team
            {
                Name = "Team 2",
            },

            new Team
            {
                Name = "Team 3",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateTeamMembers(PropertyManagerContext context)
    {
        context.TeamMembers.AddRange(
            new TeamMember
            {
                UserId = _context.Users.First(x => x.Name == "Lucas B").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = _context.Users.First(x => x.Name == "Erik G").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = _context.Users.First(x => x.Name == "Johannes Å").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = _context.Users.First(x => x.Name == "Erik G").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = true,
            },

            new TeamMember
            {
                UserId = _context.Users.First(x => x.Name == "Alex A").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateCustomerChores(PropertyManagerContext context)
    {
        context.CustomerChores.AddRange(
            new CustomerChore
            {
                CustomerId = _context.Customers.First(x => x.Name == "BRF Motorn").Id.ToString(),
                ChoreId = _context.Chores.First(x => x.Title == "Vårluckring i rabatt").Id.ToString(),
                Frequency = 1,
                PeriodicId = _context.Periodics.First(x => x.Name == "Månadsvis").Id.ToString(),
            },

            new CustomerChore
            {
                CustomerId = _context.Customers.First(x => x.Name == "BRF Motorn").Id.ToString(),
                ChoreId = _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString(),
                Frequency = 4,
                PeriodicId = _context.Periodics.First(x => x.Name == "Årligen").Id.ToString(),
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateChoreStatuses(PropertyManagerContext context)
    {
        context.ChoreStatuses.AddRange(
            new ChoreStatus
            {
                CustomerChoreId = _context.CustomerChores.FirstOrDefault(x => x.ChoreId == _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString()).Id.ToString(),
                StartDate = DateTime.Today,
                CompletedDate = DateTime.Today,
                DoneBy = "UserId",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateCustomers(PropertyManagerContext context)
    {
        context.Customers.AddRange(
            new Customer
            {
                AreaId = _context.Areas.First(x => x.Name == "Norrmalm").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                Name = "BRF Motorn",
                Address = "Storgatan 11B, Skövde"
            },
            new Customer
            {
                AreaId = _context.Areas.First(x => x.Name == "Billingelund").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                Name = "BRF Billingen",
                Address = "Storgatan 11B, Skövde"
            },
            new Customer
            {
                AreaId = _context.Areas.First(x => x.Name == "Käpplunda").Id.ToString(),
                TeamId = _context.Teams.First(x => x.Name == "Team 3").Id.ToString(),
                Name = "BRF Käpplunda",
                Address = "Havstenavägen 30A, Skövde"
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateChores(PropertyManagerContext context)
    {
        context.Chores.AddRange(
            new Chore
            {
                Title = "Vårluckring i rabatt",
                Description = "Luckra rabatterna",
                CategoryId = Guid.NewGuid().ToString(),
            },

            new Chore
            {
                Title = "Beskärning buskar",
                Description = "Beskär buskarna",
                CategoryId = Guid.NewGuid().ToString(),
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateUsers(PropertyManagerContext context)
    {
        context.Users.AddRange(
            new User
            {
                CredId = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid().ToString(),
                Name = "Alex A",
            },
            new User
            {
                CredId = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid().ToString(),
                Name = "Erik G",
            },
            new User
            {
                CredId = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid().ToString(),
                Name = "Johannes Å",
            },
            new User
            {
                CredId = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid().ToString(),
                Name = "Lucas B",
            });

        await _context.SaveChangesAsync();
    }
    private static async void GenerateChoreComments(PropertyManagerContext context)
    {
        context.ChoreComments.AddRange(
            new ChoreComment
            {
               
                CustomerChoreId = _context.CustomerChores.FirstOrDefault(x => x.ChoreId == _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString()).Id.ToString(),
                UserId = _context.Users.First(x => x.Name == "Alex A").Id.ToString(),
                Message = "Låg jordnivå",
                Time = new DateTime(2023, 01, 03, 12, 00, 00)
            },
            new ChoreComment
            {
                CustomerChoreId = _context.CustomerChores.FirstOrDefault(x => x.ChoreId == _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString()).Id.ToString(),
                UserId =  _context.Users.First(x => x.Name == "Lucas B").Id.ToString(),
                Message = "Ställde skyffeln bakom förrådet",
                Time = new DateTime(2023, 01, 03, 15, 00, 00)
            });

        await _context.SaveChangesAsync();
    }
}