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

    [HttpPut]
    [Route("UpdateTeamMembers/")]
    public async Task<ActionResult<IList<TeamMember>>> PutTeamMembersAsync(PutTeamMembersRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PutTeamMembersRequestDto, UpdateTeamMembersCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
}