using Core.Domain;
using Core.Domain.Report;
using Core.Features.Queries.CustomerChores;
using Core.Features.Queries.Customers;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Globalization;

namespace Core.Features.Queries.Report;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, ReportObject>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IArchivedChoreStatusRepository _archRepo;

    public GetReportQueryHandler(IMapper mapper, IMediator mediator, IArchivedChoreStatusRepository archRepo)
    {
        _archRepo = archRepo;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ReportObject> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        return await GenerateReportObjectAsync(request, cancellationToken);
    }

    private async Task<ReportObject> GenerateReportObjectAsync(GetReportQuery request, CancellationToken cancellationToken = default)
    {
        var dateNow = DateTime.Now;
        var choreRows = new List<ChoreRow>();
        var customerInfo = _mapper.Map<CustomerInfo>(await _mediator.Send(new GetCustomerByIdQuery { Id = request.CustomerId }));
        var issuerInfo = new IssuerInfo
        {
            Id = new Guid(),
            Name = "HSB Norra Götaland",
            Address = "Property Manager Street 1",
            Email = "ng@hsb.se",
            PhoneNumber = "0707123456"
        };


        var customerChores = await _mediator.Send(new GetCustomerChoresByCustomerIdQuery { Id = request.CustomerId }, cancellationToken);

        foreach (var customerChore in customerChores)
        {
            var allAchivedChores = await _archRepo.GetQuery(x => x.CustomerChoreId == customerChore.Id.ToString());
            var monthResult = new List<MonthResult>();


            for (var i = 1; i <= dateNow.Month; i++)
            {
                var monthNr = i;
                var thisMonthResult = new MonthResult
                {
                    MonthNr = monthNr,
                    Progress = "-"
                };

                var achivedChoreStatuses = allAchivedChores.Where(x => x.CompletedDate.Month == monthNr && x.CompletedDate.Year == dateNow.Year);
                decimal progress = achivedChoreStatuses.Count() / customerChore.Frequency;
                var test = CheckStatusOK(achivedChoreStatuses.Count(), customerChore.Frequency, customerChore.Periodic, monthNr);

                Console.WriteLine("Test: " + test);

                if (progress == 0m)
                {
                    thisMonthResult.Progress = "-";
                }

                if (progress > 0m && progress < 1m)
                {
                    thisMonthResult.Progress = $"{achivedChoreStatuses.Count()} / {customerChore.Frequency}";
                }

                if (progress >= 1m)
                {
                    thisMonthResult.Progress = "OK";
                }
                monthResult.Add(thisMonthResult);
            }

            var choreRow = new ChoreRow
            {
                ChoreName = customerChore.Chore.Title,
                MonthResult = monthResult
            };

            choreRows.Add(choreRow);
        }

        return new ReportObject
        {
            Id = new Random().Next(0, 9999999),
            CustomerInfo = customerInfo,
            IssuerInfo = issuerInfo,
            ChoreRows = choreRows
        };

    }

    private static decimal CheckStatusOK(int choresDone, int frequency, Periodic periodic, int monthNr)
    {
        var year = DateTime.Now.Year;

        var calendar = new GregorianCalendar();
        int weeksInMonth = calendar.GetWeekOfYear(new DateTime(year, monthNr, 1), CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        int daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, monthNr))
            .Select(day => new DateTime(year, monthNr, day))
            .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);

        Console.WriteLine(periodic.Name);
        return periodic.Id.ToString() switch
        {
            // Daily
            "6015CC43-A50F-4415-A369-08DB578342EB" => choresDone / (frequency * daysInMonth),
            // Weekly
            "90B6F72A-DCF8-4301-A36A-08DB578342EB" => (choresDone / frequency) * weeksInMonth,
            // Monthly
            "AE0C808E-1A8A-45E7-A36B-08DB578342EB" => choresDone / frequency,
            // Yearly
            "AD68415B-1E99-4210-A36C-08DB578342EB" => choresDone / frequency,
            _ => (decimal)666,
        };
    }
}
