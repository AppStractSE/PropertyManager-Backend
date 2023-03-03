using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Domain.Features.Queries.Categories;
using Api.Dto.Response.Category.v1;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<CategoryController> _logger;

    public CategoryController(IMediator mediator, IMapper mapper, ILogger<CategoryController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;

    }
    [HttpGet]
    public async Task<ActionResult<IList<CategoryResponseDto>>> GetAllCategories()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Category controller: GetAllCategories");
            return BadRequest(ex.Message);
        }
    }
}