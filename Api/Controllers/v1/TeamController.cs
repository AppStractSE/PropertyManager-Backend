using Core.Features.Queries.Teams;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Core.Features.Commands.Team;
using Api.Dto.Request.Team.v1;
using Api.Dto.Response.Team.v1;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamController> _logger;

    public TeamController(IMediator mediator, IMapper mapper, ILogger<TeamController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Team>>> GetAllTeams()
    {
        try
        {
            var result = await _mediator.Send(new GetAllTeamsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Team controller: GetAllTeams");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetTeamById/")]
    public async Task<ActionResult<TeamResponseDto>> GetTeamById([FromQuery] GetTeamByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetTeamByIdRequestDto, GetTeamByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<TeamResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Team controller: GetTeamById");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Team>> PostTeamAsync(PostTeamRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostTeamRequestDto, AddTeamCommand>(request));
            return Ok(result);    
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Team>> PutTeamAsync(PutTeamRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PutTeamRequestDto, UpdateTeamCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteTeamById/")]
    public async Task<ActionResult> DeleteTeamById([FromQuery] DeleteTeamByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<DeleteTeamByIdRequestDto, DeleteTeamCommand>(request));
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Team controller: Could not delete");
            return BadRequest(ex.Message);
        }
    }
}