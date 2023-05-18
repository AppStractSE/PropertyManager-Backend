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
        if (!context.Cities.Any()) await GenerateCities(context);
        if (!context.Areas.Any()) await GenerateAreas(context);
        if (!context.Teams.Any()) await GenerateTeams(context);
        // if (!context.TeamMembers.Any()) await GenerateTeamMembers(context);
        if (!context.Customers.Any()) await GenerateCustomers(context);
        if (!context.Categories.Any()) await GenerateCategories(context);
        if (!context.SubCategories.Any()) await GenerateSubCategories(context);
        if (!context.Chores.Any()) await GenerateChores(context);
        if (!context.CustomerChores.Any()) await GenerateCustomerChores(context);
        // if (!context.ChoreComments.Any()) await GenerateChoreComments(context);
        // if (!context.ChoreStatuses.Any()) await GenerateChoreStatuses(context);
    }

    private static async Task GenerateSubCategories(PropertyManagerContext context)
    {
        context.SubCategories.AddRange(
            new SubCategory
            {
                Title = "Vegetationsyta",
                CategoryId = _context.Categories.First(x => x.Title == "T1").Id,
                Reference = "T1.1"
            },
            new SubCategory
            {
                Title = "Yttertak, skärmtak och dylikt",
                CategoryId = _context.Categories.First(x => x.Title == "T2").Id,
                Reference = "T2.1"
            },
            new SubCategory
            {
                Title = "Fasader",
                CategoryId = _context.Categories.First(x => x.Title == "T2").Id,
                Reference = "T2.2"
            },
            new SubCategory
            {
                Title = "Driftutrymmen",
                CategoryId = _context.Categories.First(x => x.Title == "T3").Id,
                Reference = "T3.1"
            }
            );

        await _context.SaveChangesAsync();
    }

    private static async Task GenerateCategories(PropertyManagerContext context)
    {
        context.Categories.AddRange(
            new Category
            {
                Title = "T1",
                Description = "Utemiljö",
            },
            new Category
            {
                Title = "T2",
                Description = "Byggnad utvändigt",
            },

            new Category
            {
                Title = "T3",
                Description = "Byggnad invändigt",
            });

        await _context.SaveChangesAsync();
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
                CityId = _context.Cities.First(x => x.Name == "Skövde").Id.ToString(),
            },
            new Area
            {
                Name = "Käpplunda",
                CityId = _context.Cities.First(x => x.Name == "Skövde").Id.ToString(),
            },

            new Area
            {
                Name = "Billingelund",
                CityId = _context.Cities.First(x => x.Name == "Skövde").Id.ToString(),
            });

        await _context.SaveChangesAsync();
    }

    private static async Task GenerateCities(PropertyManagerContext context)
    {
        context.Cities.AddRange(
            new City
            {
                Name = "Skövde",
            },
            new City
            {
                Name = "Tidaholm",
            },

            new City
            {
                Name = "Götene",
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
        //ADD USERID
        throw new NotImplementedException();
        context.TeamMembers.AddRange(
            new TeamMember
            {
                UserId = "",
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "",
                TeamId = _context.Teams.First(x => x.Name == "Team 1").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "",
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = false,
            },
            new TeamMember
            {
                UserId = "",
                TeamId = _context.Teams.First(x => x.Name == "Team 2").Id.ToString(),
                IsTemporary = true,
            },

            new TeamMember
            {
                UserId = "",
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
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Vegetationsyta").Id.ToString(),
            },

            new Chore
            {
                Title = "Beskärning buskar",
                Description = "Beskär buskarna",
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Vegetationsyta").Id.ToString(),
            },
            new Chore
            {
                Title = "Takavvattning",
                Description = "Ta bort vattnet från taket",
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Yttertak, skärmtak och dylikt").Id.ToString(),
            },
            new Chore
            {
                Title = "Rengör altan",
                Description = "Enskilda terrasser räcken, inglasningar, stag, infästningar, plåtbeslag, anslutningar och fogar.",
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Fasader").Id.ToString(),
            },
            new Chore
            {
                Title = "Rengör terrass",
                Description = "Enskilda terrasser räcken, inglasningar, stag, infästningar, plåtbeslag, anslutningar och fogar.",
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Fasader").Id.ToString(),
            },
            new Chore
            {
                Title = "Sopa avfallsrum",
                Description = "Avfallsrum för hushållssopor, grovsopor och återvinningsprodukter. Renhållning behandlas under denna rubrik, för övrigt, se T7.1.",
                SubCategoryId = _context.SubCategories.First(x => x.Title == "Driftutrymmen").Id.ToString(),
            }
            );

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