using Domain.Features.Queries.Chores;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Response.Chore.v1;
using Api.Dto.Request.Chore.v1;
using Domain.Features.Commands.Chore;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChoreController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<ChoreController> _logger;

    public ChoreController(IMediator mediator, IMapper mapper, ILogger<ChoreController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger= logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Chore>>> GetAllChores()
    {
        try
        {
            var result = await _mediator.Send(new GetAllChoresQuery());
            return Ok(result);          
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Chore controller: GetAllChores", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetChoreById/")]
    public async Task<ActionResult<ChoreResponseDto>> GetChoreById([FromQuery]GetChoreByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetChoreByIdRequestDto, GetChoreByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<ChoreResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Chore controller: GetChoreById", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Chore>> PostChoreAsync(PostChoreRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostChoreRequestDto, AddChoreCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Chore controller: PostChoreAsync", ex);
            return BadRequest(ex.Message);
        }
    }
}