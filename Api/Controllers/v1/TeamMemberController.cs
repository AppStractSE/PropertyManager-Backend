using Core.Features.Queries.TeamMembers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Api.Dto.Request.TeamMember.v1;
using Core.Features.Commands.TeamMember;
using Api.Dto.Response.TeamMember.v1;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IList<TeamMemberResponseDto>>> GetAllTeamMembers()
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
    [Authorize]
    [HttpGet]
    [Route("GetTeamMembersByUserId/")]
    public async Task<ActionResult<IList<TeamMemberResponseDto>>> GetTeamMembersByUserId([FromQuery] GetTeamMembersByUserIdRequestDto request)
    {
        try
        {
            var response = await _mediator.Send(_mapper.Map<GetTeamMembersByUserIdRequestDto, GetTeamMembersByUserIdQuery>(request));
            var result = _mapper.Map<IList<TeamMemberResponseDto>>(response);
            return result != null ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in TeamMember controller: GetTeamMemberByUserId");
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("AddTeamMember/")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("AddTeamMembers/")]
    public async Task<ActionResult<IList<TeamMember>>> PostTeamMembersAsync(PostTeamMembersRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostTeamMembersRequestDto, AddTeamMembersCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("UpdateTeamMember/")]
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

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("UpdateTeamMembers/")]
    public async Task<ActionResult<IList<TeamMember>>> PutTeamMembersAsync(PutTeamMembersRequestDto request)
    {
        try
        {
            if (request.TeamMembers == null || request.TeamMembers.Count == 0)
            {
                await _mediator.Send(_mapper.Map<PutTeamMembersRequestDto, BulkDeleteTeamMembersCommand>(request));
                return Ok();
            }

            var result = await _mediator.Send(_mapper.Map<PutTeamMembersRequestDto, UpdateTeamMembersCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}