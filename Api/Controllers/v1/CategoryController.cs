using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Features.Queries.Categories;
using Api.Dto.Response.Category.v1;
using Microsoft.AspNetCore.Authorization;
using Core.Domain;
using Api.Dto.Request.Category.v1;
using Core.Features.Commands.Category;
using Api.Dto.Request.SubCategory.v1;
using Core.Features.Commands.SubCategory;

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

    [Authorize]
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

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategoryAsync(PostCategoryRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostCategoryRequestDto, AddCategoryCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("PostSubCategory/")]

    public async Task<ActionResult<SubCategory>> PostSubCategoryAsync(PostSubCategoryRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostSubCategoryRequestDto, AddSubCategoryCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}