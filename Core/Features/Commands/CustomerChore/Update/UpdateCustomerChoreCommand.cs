using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class UpdateCustomerChoreCommand : IRequest<Domain.CustomerChore>
{
    public Guid Id { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
    public string? Description { get; set; }

}