using MediatR;

namespace Core.Features.Commands.SubCategory;

public class AddSubCategoryCommand : IRequest<Domain.SubCategory>
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}