using Core.Domain.Report;
using Core.Features.Queries.CustomerChores;
using Core.Features.Queries.Customers;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Report;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, byte[]>
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

    public async Task<byte[]> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var reportObject = await GenerateReportObjectAsync(request.CustomerId, cancellationToken);


        // Add logic to generate report here

        // TODO:
        // 1. Create a new instance of the report (X)
        // 2. Generate the report object (X)
        // 3. Generate the report in xls <---
        // 4. Return the report as a byte array (/)
        // 5. Add a new endpoint in the controller to return the report as a file 
        // Done!

        return await Task.FromResult(new byte[0]);
    }

    private async Task<ReportObject> GenerateReportObjectAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var dateNow = DateTime.Now;
        var choreRows = new List<ChoreRow>();
        var customerInfo = _mapper.Map<CustomerInfo>(await _mediator.Send(new GetCustomerByIdQuery { Id = customerId }));
        var issuerInfo = new IssuerInfo
        {
            Id = new Guid(),
            Name = "Property Manager",
            Address = "Property Manager Street 1",
            Email = "Property Manager City",
            PhoneNumber = "12345"
        };


        var customerChores = await _mediator.Send(new GetCustomerChoresByCustomerIdQuery { Id = customerId });

        foreach (var customerChore in customerChores)
        {
            var allAchivedChores = await _archRepo.GetQuery(x => x.CustomerChoreId == customerChore.Id.ToString());
            var monthResult = new List<MonthResult>();
            for (var i = 0; i < dateNow.Month; i++)
            {
                var monthNr = i + 1;
                var thisMonthResult = new MonthResult
                {
                    MonthNr = monthNr,
                    Progress = "Not Done"
                };

                var achivedChore = allAchivedChores.Select(x => x.CompletedDate.Month == monthNr && x.CompletedDate.Year == dateNow.Year);
                var progress = (decimal)(achivedChore.Count() / customerChore.Frequency);

                if (progress > 0)
                {
                    thisMonthResult.Progress = "0";
                }

                if (progress > 0 && progress < 1)
                {
                    thisMonthResult.Progress = $"{achivedChore.Count()} / {customerChore.Frequency}";
                }

                if (progress == 1)
                {
                    thisMonthResult.Progress = "OK";
                }
                monthResult.Add(thisMonthResult);
            }

            var choreRow = new ChoreRow
            {
                MonthResult = monthResult
            };

            choreRows.Add(choreRow);
        }

        return new ReportObject
        {
            Id = new Random().Next(0,9999999),
            CustomerInfo = customerInfo,
            IssuerInfo = issuerInfo,
            ChoreRows = choreRows
        };

    }
}
