using MediatR;

namespace Domain.Features.Commands.ChoreComment;

public class DeleteChoreCommentCommand : IRequest<Domain.ChoreComment>
{
    public Guid Id { get; set; }
}