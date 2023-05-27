using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Categories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IList<Category>>
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;
    public GetAllCategoriesQueryHandler(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categoryEntities = await _repo.GetAllAsync();
        var categories = _mapper.Map<IList<Category>>(categoryEntities);
        foreach (var category in categories)
        {
            var categoryChildren = categoryEntities.Where(x => x.ParentId == category.Id).ToList();
            category.IsParent = category.ParentId == default || categoryChildren.Any();
            category.SubCategories = _mapper.Map<IList<Category>>(categoryChildren);
        }
        return categories;
    }
}