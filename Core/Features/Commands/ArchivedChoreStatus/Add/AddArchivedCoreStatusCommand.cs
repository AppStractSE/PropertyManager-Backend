using MediatR;

namespace Core.Features.Commands.ArchivedChoreStatus;

public class AddArchivedChoreStatusCommand : IRequest<Domain.ArchivedChoreStatus>
{
    public string CustomerChoreId { get; set; }
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}