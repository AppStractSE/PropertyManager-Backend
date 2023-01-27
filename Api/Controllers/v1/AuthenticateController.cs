using Api.Dto.Response.Authenticate.v1;
using Domain.Domain.Authentication;
using Domain.Features.Authentication.Queries;
using Domain.Features.Commands.Authentication.Create;
using Domain.Features.Queries.Authentication.GetAllUsers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<AuthenticateController> _logger;

    public AuthenticateController(IMapper mapper, IMediator mediator, ILogger<AuthenticateController> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthUser))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var authUser = await _mediator.Send(_mapper.Map<LoginModel, PostLoginCommand>(model));
            if (authUser == null)
            {
                return Unauthorized();
            }
            return Ok(authUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Auth: Login");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = UserRoles.Admin +", "+ UserRoles.SuperAdmin)]
    [HttpGet]
    [Route("getAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserInfoDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            var users = _mapper.Map<IList<UserInfoDto>>(result);
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Auth: GetAllUsers");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("validation")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetValidation()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var authUser = await _mediator.Send(new GetTokenValidationQuery() { Token = token });
            if (authUser == null)
            {
                return BadRequest();
            }
            return Ok(authUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Auth: Authenticate");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<RegisterModel, PostRegisterCommand>(model));

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
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Auth: Register");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("register-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        try
        {
            var mappedModel = _mapper.Map<RegisterModel, PostRegisterCommand>(model);
            mappedModel.Role = UserRoles.Admin;

            var result = await _mediator.Send(mappedModel);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                });
            }
            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Auth: Login");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}