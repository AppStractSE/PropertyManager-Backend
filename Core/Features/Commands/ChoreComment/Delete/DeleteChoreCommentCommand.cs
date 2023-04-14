using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class DeleteChoreCommentCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}