using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ArchivedChoreStatus;

public class AddArchivedChoreStatusCommandHandler : IRequestHandler<AddArchivedChoreStatusCommand, Domain.ArchivedChoreStatus>
{
    private readonly IArchivedChoreStatusRepository _repo;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddArchivedChoreStatusCommandHandler(IArchivedChoreStatusRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.ArchivedChoreStatus> Handle(AddArchivedChoreStatusCommand request, CancellationToken cancellationToken)
    {
        if (request.CompletedDate == null || request.CompletedDate == DateTime.MinValue)
        {
            request.CompletedDate = DateTime.Now;
        }
        else
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            request.CompletedDate = request.CompletedDate.AddHours(offset.Hours).AddMinutes(offset.Minutes);
        }
    
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ArchivedChoreStatus>(request));
        return _mapper.Map<Domain.ArchivedChoreStatus>(response);
    }
}