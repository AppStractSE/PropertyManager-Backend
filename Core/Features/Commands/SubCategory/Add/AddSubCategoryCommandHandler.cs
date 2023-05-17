using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.SubCategory;

public class AddSubCategoryCommandHandler : IRequestHandler<AddSubCategoryCommand, Domain.SubCategory>
{
    private readonly ISubCategoryRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddSubCategoryCommandHandler(ISubCategoryRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Domain.SubCategory> Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("SubCategories:");
        await _cache.RemoveAsync("SubCategories:Categories");
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.SubCategory>(request));
        return _mapper.Map<Domain.SubCategory>(response);
    }
}