using Domain.Features.Queries.TeamMembers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.TeamMember.v1;
using Domain.Features.Commands.TeamMember;
using Api.Dto.Response.TeamMember.v1;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class TeamMemberController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamMemberController> _logger;

    public TeamMemberController(IMediator mediator, IMapper mapper, ILogger<TeamMemberController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<TeamMember>>> GetAllTeamMembers()
    {
        try
        {
            var result = await _mediator.Send(new GetAllTeamMembersQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in TeamMember controller: GetAllTeamMembers");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetTeamMemberById/")]
    public async Task<ActionResult<TeamMemberResponseDto>> GetTeamMemberById([FromQuery] GetTeamMemberByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetTeamMemberByIdRequestDto, GetTeamMemberByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<TeamMemberResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in TeamMember controller: GetTeamMemberById");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TeamMember>> PostTeamMemberAsync(PostTeamMemberRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostTeamMemberRequestDto, AddTeamMemberCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<TeamMember>> PutTeamMemberAsync(PutTeamMemberRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PutTeamMemberRequestDto, UpdateTeamMemberCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}