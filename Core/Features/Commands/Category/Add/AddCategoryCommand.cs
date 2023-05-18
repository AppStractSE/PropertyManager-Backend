using MediatR;

namespace Core.Features.Commands.Category;

public class AddCategoryCommand : IRequest<Domain.Category>
{
    public string Title { get; set; }
    public string Description { get; set; }
}