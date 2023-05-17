using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Api.Dto.Request.SubCategory.v1;
using Core.Features.Commands.SubCategory;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class SubCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<SubCategoryController> _logger;

    public SubCategoryController(IMediator mediator, IMapper mapper, ILogger<SubCategoryController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;

    }

    [HttpPost]
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