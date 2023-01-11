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

    public TeamMemberController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<TeamMember>>> GetAllTeamMembers()
    {
        var result = await _mediator.Send(new GetAllTeamMembersQuery());
        return Ok(result);
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
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TeamMember>> PostTeamMemberAsync(PostTeamMemberRequestDto request)
    {
        var result = await _mediator.Send(_mapper.Map<PostTeamMemberRequestDto, AddTeamMemberCommand>(request));
        return Ok(result);
    }

    [HttpPatch]
    public async Task<ActionResult<TeamMember>> PatchTeamMemberAsync(PatchTeamMemberRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PatchTeamMemberRequestDto, UpdateTeamMemberCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}