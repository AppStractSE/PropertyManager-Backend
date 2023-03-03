using MediatR;

namespace Domain.Features.Queries.Categories;

public class GetAllCategoriesQuery : IRequest<IList<Domain.Category>>
{

}