using Api.Dto.Request.UserData.v1;
using Api.Dto.Response.UserData.v1;
using Core.Features.Queries.UserData;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;
public class UserDataController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<UserDataController> _logger;

    public UserDataController(IMediator mediator, IMapper mapper, ILogger<UserDataController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("GetUserDataById/")]
    public async Task<ActionResult<UserDataResponseDto>> GetUserDataById([FromQuery] GetUserDataByUserIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetUserDataByUserIdQuery>(request));
            return result != null ? Ok(_mapper.Map<UserDataResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in User controller: GetUserDataById");
            return BadRequest(ex.Message);
        }
    }
}