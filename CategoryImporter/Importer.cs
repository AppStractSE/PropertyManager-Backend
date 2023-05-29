using CategoryImporter.Domain;
using Core.Repository.Entities;
using Core.Repository.Interfaces;
using CsvHelper;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace CategoryImporter
{
    internal class Importer : IHostedService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IChoreRepository _choreRepo;

        public Importer(IChoreRepository choreRepo, ICategoryRepository categoryRepo)
        {
            _choreRepo = choreRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RunAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private async Task RunAsync()
        {

            var basePath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(basePath + "\\imports"))
            {
                Directory.CreateDirectory(basePath + "\\imports");
            }

            Console.WriteLine("Please enter name of the csv-file to import");
            var fileName = Console.ReadLine();

            while (!File.Exists($"{basePath}\\imports\\{fileName}.csv"))
            {
                Console.WriteLine($"Please add {fileName}.csv to imports-directory. Press Enter to continue");
                Console.ReadLine();

            }

            var files = Directory.GetFiles($"{basePath}\\imports");
            var chores = new List<Chore>();
            var categories = new List<Category>();

            using var reader = new StreamReader($"D:\\Repos\\PropertyManager-Project\\PropertyManager-Backend\\CategoryImporter\\bin\\Debug\\net6.0\\imports\\{fileName}.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<AFFObject>().ToList();

            foreach (var record in records)
            {
                if (record.Reference.Any(x => char.IsLetter(x)))
                {
                    var category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Reference = record.Reference,
                        Title = record.Title,
                        ParentId = categories.FirstOrDefault(x => x.Reference == record.ParentCategory)?.Id ?? default
                    };
                    categories.Add(category);
                }
                else
                {
                    var hasRefnr = int.TryParse(record.Reference, out var refnr);
                    var chore = new Chore
                    {
                        Id = Guid.NewGuid(),
                        Title = string.IsNullOrEmpty(record.Title) ? record.ChoresCategory + record.Reference : record.Title,
                        Description = record.Description,
                        Reference = hasRefnr ? refnr : 0,
                        SubCategoryId = categories.FirstOrDefault(x => x.Reference == record.ChoresCategory)?.Id.ToString()
                    };
                    chores.Add(chore);
                }
            }

            Console.WriteLine("-------- " + chores.Count + " Chores----------");
            foreach (var chore in chores)
            {
                Console.WriteLine($"Reference: {chore.Reference}, Chore: {chore.Title}, Description: {chore.Description}, SubCategoryId: {chore.SubCategoryId}");
            }

            Console.WriteLine("--------- " + categories.Count + " Categories----------");
            foreach (var category in categories)
            {
                Console.WriteLine($"Category: {category.Title}, Reference: {category.Reference}, ParentId: {category.ParentId}");
            }
            Console.WriteLine("Do you want to import this data? (y/n)");
            var confirmation = Console.ReadLine();
            var isOk = (confirmation == "y" || confirmation == "Y" || confirmation == "yes" || confirmation == "Yes");

            if (isOk)
            {
                await _choreRepo.AddRangeAsync(chores);
                await _categoryRepo.AddRangeAsync(categories);
                Console.WriteLine("All Done! Press Enter to exit");
                Console.ReadLine();
            }

        }
    }
}
