using Domain.Features.Queries.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.User.v1;
using Domain.Features.Commands.User;

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

    [HttpPost]
    public async Task<ActionResult<User>> PostChoreCommentAsync(PostUserRequestDto request)
    {
        var result = await _mediator.Send(_mapper.Map<PostUserRequestDto, AddUserCommand>(request));
        return Ok(result);
    }
}