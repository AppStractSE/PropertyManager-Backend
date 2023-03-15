using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class BulkDeleteChoreCommentsCommand : IRequest<bool>
{
    public string CustomerChoreId { get; set; }
}