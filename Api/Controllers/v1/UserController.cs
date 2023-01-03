using Domain.Features.Queries.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.User.v1;
using Domain.Features.Commands.User;
using Api.Dto.Response.User.v1;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IList<User>>> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpGet]
    [Route("GetUserById/")]
    public async Task<ActionResult<UserResponseDto>> GetUserById([FromQuery]GetUserByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetUserByIdRequestDto, GetUserByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<UserResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUserCommentAsync(PostUserRequestDto request)
    {
        var result = await _mediator.Send(_mapper.Map<PostUserRequestDto, AddUserCommand>(request));
        return Ok(result);
    }
}