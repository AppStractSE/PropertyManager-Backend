using Domain.Features.Queries.ChoreComments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.ChoreComment.v1;
using Domain.Features.Commands.ChoreComment;
using Api.Dto.Response.ChoreComment.v1;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreCommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ChoreCommentController> _logger;

    public ChoreCommentController(IMediator mediator, IMapper mapper, ILogger<ChoreCommentController> logger )
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<ChoreComment>>> GetAllChoreComments()
    {
        try
        {
            var result = await _mediator.Send(new GetAllChoreCommentsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in ChoreComment controller: GetAllChoreComments");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetChoreCommentsByCustomerChoreId")]
    public async Task<ActionResult<IList<ChoreCommentResponseDto>>> GetCustomerChoresByCustomer([FromQuery] GetChoreCommentsByCustomerChoreIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetChoreCommentsByCustomerChoreIdRequestDto, GetChoreCommentsByCustomerChoreIdQuery>(request));
            return result.Count > 0 ? Ok(_mapper.Map<IList<ChoreCommentResponseDto>>(result)) : new List<ChoreCommentResponseDto>();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ChoreComment>> PostChoreCommentAsync(PostChoreCommentRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostChoreCommentRequestDto, AddChoreCommentCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }
}