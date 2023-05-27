using MediatR;

namespace Core.Features.Commands.Category;

public class AddCategoryCommand : IRequest<Domain.Category>
{
    public string? ParentId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}