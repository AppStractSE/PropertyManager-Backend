using MediatR;

namespace Core.Features.Queries.Report;

public class GetExcelReportQuery : IRequest<byte[]>
{
    public string CustomerId { get; set; }
}
