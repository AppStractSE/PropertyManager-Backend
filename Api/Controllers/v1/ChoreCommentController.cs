using Domain.Features.Queries.ChoreComments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.ChoreComment.v1;
using Domain.Features.Commands.ChoreComment;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreCommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChoreCommentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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