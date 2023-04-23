using Core.Features.Queries.Chores;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Api.Dto.Response.Chore.v1;
using Api.Dto.Request.Chore.v1;
using Core.Features.Commands.Chore;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1;

[Authorize]
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
    public async Task<ActionResult<IList<ChoreResponseDto>>> GetAllChores()
    {
        try
        {
            var result = await _mediator.Send(new GetAllChoresQuery());
            return Ok(result);          
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Chore controller: GetAllChores");
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
            _logger.LogError(message: "Error in Chore controller: GetChoreById");
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
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
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Chore>> PutChoreAsync(PutChoreRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PutChoreRequestDto, UpdateChoreCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}