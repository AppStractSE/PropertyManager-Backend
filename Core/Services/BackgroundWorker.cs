using Core.Repository.Entities;
using Core.Repository.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class BackgroundWorker : BackgroundService
{
    private readonly ILogger<BackgroundWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private DateTime _lastRun = DateTime.MinValue;

    public BackgroundWorker(ILogger<BackgroundWorker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            await ArchiveChoreStatuses(stoppingToken);
            await Task.Delay(30000, stoppingToken);
        }
    }

    private async Task ArchiveChoreStatuses(CancellationToken stoppingToken)
    {
        var dateNow = DateTime.Now;

        if (dateNow.Date == _lastRun.Date)
        {
            _logger.LogInformation("Worker already ran today at: {time}", DateTime.Now);
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();

        var customerchores = await scope.ServiceProvider.GetRequiredService<ICustomerChoreRepository>().GetAllAsync();
        var periodics = await scope.ServiceProvider.GetRequiredService<IPeriodicRepository>().GetAllAsync();

        var yearlyChores = customerchores.Where(x => x.PeriodicId == periodics.FirstOrDefault(y => y.Name == "Årligen").Id.ToString());
        var monthlyChores = customerchores.Where(x => x.PeriodicId == periodics.FirstOrDefault(y => y.Name == "Månadsvis").Id.ToString());
        var weeklyChores = customerchores.Where(x => x.PeriodicId == periodics.FirstOrDefault(y => y.Name == "Veckovis").Id.ToString());
        var dailyChores = customerchores.Where(x => x.PeriodicId == periodics.FirstOrDefault(y => y.Name == "Dagligen").Id.ToString());

        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        await MoveToArchiveAsync(dailyChores);

        if (dateNow.DayOfWeek == DayOfWeek.Sunday) await MoveToArchiveAsync(weeklyChores);
        if (dateNow.Day == 1) await MoveToArchiveAsync(monthlyChores);
        if (dateNow.DayOfYear == 1) await MoveToArchiveAsync(yearlyChores);

        _lastRun = dateNow;
    }

    private async Task MoveToArchiveAsync(IEnumerable<CustomerChore> chores)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var archivedRepo = scope.ServiceProvider.GetRequiredService<IArchivedChoreStatusRepository>();
        var chorestatusRepo = scope.ServiceProvider.GetRequiredService<IChoreStatusRepository>();

        var statusesToMove = chorestatusRepo.GetAllAsync().GetAwaiter().GetResult().Where(x => chores.Any(y => y.Id.ToString() == x.CustomerChoreId));
        await archivedRepo.AddRangeAsync(mapper.Map<List<ArchivedChoreStatus>>(statusesToMove));
        await chorestatusRepo.DeleteRangeAsync(statusesToMove);
    }
}