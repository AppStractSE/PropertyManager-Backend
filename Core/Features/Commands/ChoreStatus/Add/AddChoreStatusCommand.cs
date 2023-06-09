using MediatR;

namespace Core.Features.Commands.ChoreStatus;

public class AddChoreStatusCommand : IRequest<Domain.ChoreStatus>
{
    public string CustomerChoreId { get; set; }
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}