using MediatR;

namespace Core.Features.Queries.Categories;

public class GetAllCategoriesQuery : IRequest<IList<Domain.Category>>
{
}