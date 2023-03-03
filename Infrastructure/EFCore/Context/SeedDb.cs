using Core.Repository.Entities;
using Infrastructure.Context;

namespace Infrastructure.EFCore.Context;

public class SeedDb
{
    private static PropertyManagerContext? _context;
    public static async Task SeedAsync(PropertyManagerContext context)
    {
        context.Database.EnsureCreated();
        _context = context;
        if (!context.Periodics.Any()) await GeneratePeriodics(context);
        if (!context.Areas.Any()) await GenerateAreas(context);
        if (!context.Teams.Any()) await GenerateTeams(context);
        if (!context.TeamMembers.Any()) await GenerateTeamMembers(context);
        if (!context.Customers.Any()) await GenerateCustomers(context);
        if (!context.Chores.Any()) await GenerateChores(context);
        if (!context.CustomerChores.Any()) await GenerateCustomerChores(context);
        if (!context.ChoreComments.Any()) await GenerateChoreComments(context);
        if (!context.ChoreStatuses.Any()) await GenerateChoreStatuses(context);
    }

    private static async Task GeneratePeriodics(PropertyManagerContext context)
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
    private static async Task GenerateAreas(PropertyManagerContext context)
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

    private static async Task GenerateTeams(PropertyManagerContext context)
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

    private static async Task GenerateTeamMembers(PropertyManagerContext context)
    {
        context.TeamMembers.AddRange(
            new TeamMember
            {
                UserId = "609dd774-cdc1-4965-83ce-804d2fe1e24a",
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "adf7ff92-371e-4b9c-b2d7-e7fc607bf94c",
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "4080fd0d-3ff3-48a6-ab09-ce46dfd77cf7",
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "8a0fc6ca-61f7-4b9f-b9e0-cd5daf852767",
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = true,
            },

            new TeamMember
            {
                UserId = "b4bbe1ab-6562-4d41-8d2f-56620d63942f",
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            });

        await _context.SaveChangesAsync();
    }

    private static async Task GenerateCustomerChores(PropertyManagerContext context)
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

    private static async Task GenerateChoreStatuses(PropertyManagerContext context)
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

    private static async Task GenerateCustomers(PropertyManagerContext context)
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

    private static async Task GenerateChores(PropertyManagerContext context)
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

    private static async Task GenerateChoreComments(PropertyManagerContext context)
    {
        context.ChoreComments.AddRange(
            new ChoreComment
            {

                CustomerChoreId = _context.CustomerChores.FirstOrDefault(x => x.ChoreId == _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString()).Id.ToString(),
                DisplayName = "Jonas H",
                Message = "Låg jordnivå",
                Time = new DateTime(2023, 01, 03, 12, 00, 00)
            },
            new ChoreComment
            {
                CustomerChoreId = _context.CustomerChores.FirstOrDefault(x => x.ChoreId == _context.Chores.First(x => x.Title == "Beskärning buskar").Id.ToString()).Id.ToString(),
                DisplayName = "Anders B.H",
                Message = "Ställde skyffeln bakom förrådet",
                Time = new DateTime(2023, 01, 03, 15, 00, 00)
            });

        await _context.SaveChangesAsync();
    }
}