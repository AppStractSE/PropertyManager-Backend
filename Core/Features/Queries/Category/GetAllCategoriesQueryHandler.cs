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
        var categories = _mapper.Map<IList<Category>>(await _repo.GetAllAsync());
        foreach (var category in categories)
        {
            category.SubCategories = _mapper.Map<IList<Category>>(await _repo.GetQuery(x => x.ParentId == category.Id));
        }
        return categories;
    }
}