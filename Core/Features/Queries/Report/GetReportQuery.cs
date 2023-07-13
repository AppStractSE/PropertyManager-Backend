using Core.Domain.Report;
using MediatR;

namespace Core.Features.Queries.Report;

public class GetReportQuery : IRequest<ReportObject>
{
    public string CustomerId { get; set; }
}
