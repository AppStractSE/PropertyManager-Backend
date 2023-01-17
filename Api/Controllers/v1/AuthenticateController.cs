using Domain.Domain.Authentication;
using Domain.Features.Commands.Authentication.Create;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthenticateController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var authUser = await _mediator.Send(_mapper.Map<LoginModel, PostLoginCommand>(model));
        if (authUser == null)
        {
            return Unauthorized();
        }

        return Ok(authUser);
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var result = await _mediator.Send(new PostRegisterCommand 
        { 
            Username = model.Username,
            Email = model.Email,
            Password = model.Password 
        });
        
        if(result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });
        }
        
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }

        return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var result = await _mediator.Send(new PostRegisterCommand
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Role = UserRoles.Admin
        });

        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User already exists!" });
        }

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }

        return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        
    }

    
}

