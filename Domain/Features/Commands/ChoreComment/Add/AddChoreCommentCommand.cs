using MediatR;

namespace Core.Features.Commands.ChoreComment;

public class AddChoreCommentCommand : IRequest<Domain.ChoreComment>
{
    public string Message { get; set; }
    public string CustomerChoreId { get; set; }
    public string UserId { get; set; }
    public DateTime Time { get; set; }
}