using MediatR;

namespace Core.Features.Commands.ChoreStatus;

public class BulkDeleteChoreStatusCommand : IRequest<bool>
{
    public string CustomerChoreId { get; set; }
}