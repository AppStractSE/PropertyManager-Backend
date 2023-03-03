using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Categories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IList<Category>>
{
    private readonly ICategoryRepository _repo;
    private readonly ISubCategoryRepository _subRepo;
    private readonly IMapper _mapper;
    public GetAllCategoriesQueryHandler(ICategoryRepository repo, ISubCategoryRepository subRepo, IMapper mapper)
    {
        _repo = repo;
        _subRepo = subRepo;
        _mapper = mapper;
    }
    public async Task<IList<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var subCategories = _mapper.Map<IList<SubCategory>>(await _subRepo.GetAllAsync());
        var categories = _mapper.Map<IList<Category>>(await _repo.GetAllAsync());

        foreach (var category in categories)
        {
            category.SubCategories = subCategories.Where(x => x.CategoryId == category.Id).ToList();
        }
        return categories;
    }
}