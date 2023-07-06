using MediatR;

namespace Core.Features.Queries.Report;

public class GetReportQuery : IRequest<byte[]>
{
    public string CustomerId { get; set; }
}
