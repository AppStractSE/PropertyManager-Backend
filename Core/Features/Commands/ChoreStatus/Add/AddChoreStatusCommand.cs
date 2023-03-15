using MediatR;

namespace Core.Features.Commands.ChoreStatus;

public class AddChoreStatusCommand : IRequest<Domain.ChoreStatus>
{
    public string CustomerChoreId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime CompletedDate { get; set; } = DateTime.Now;
    public string DoneBy { get; set; }
}