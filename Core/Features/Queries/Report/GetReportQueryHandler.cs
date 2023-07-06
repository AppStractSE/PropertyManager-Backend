using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Core.Domain.Report;
using System.Runtime.CompilerServices;
using Core.Features.Queries.Customers;
using Core.Features.Queries.CustomerChores;

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


        return await Task.FromResult(new byte[0]);
        }

    private async Task<ReportObject> GenerateReportObjectAsync(string customerId, CancellationToken cancellationToken = default)
    {
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
        foreach(var customerChore in customerChores)
        {
            var allAchivedChores = _archRepo.GetQuery(x => x.CustomerChoreId == customerChore.Id.ToString());
            var achivedChores = customerChores.Where(x => x.ChoreId == customerChore.ChoreId && x.).ToList();

            var choreRow = new ChoreRow
            {
                ChoreName = customerChore.Chore.Title,
                MonthResult = new Dictionary<int, string>
                {
                    { 1, "Done" },
                    { 2, "Done" },
                    { 3, "Done" },
                    { 4, "Done" },
                    { 5, "Done" },
                    { 6, "Done" },
                    { 7, "Done" },
                    { 8, "Done" },
                    { 9, "Done" },
                    { 10, "Done" },
                    { 11, "Done" },
                    { 12, "Done" }
                }
            };
            choreRows.Add(choreRow);
        }
        var achiveChores = customerChores.Where(x => x.Status == "Klar").ToList();
        
        
            new ChoreRow
            {
                ChoreName = "Chore 1",
                MonthResult = new Dictionary<int, string>
                {
                    { 1, "Done" },
                    { 2, "Done" },
                    { 3, "Done" },
                    { 4, "Done" },
                    { 5, "Done" },
                    { 6, "Done" },
                    { 7, "Done" },
                    { 8, "Done" },
                    { 9, "Done" },
                    { 10, "Done" },
                    { 11, "Done" },
                    { 12, "Done" }
                }

            }
         
    }
}
