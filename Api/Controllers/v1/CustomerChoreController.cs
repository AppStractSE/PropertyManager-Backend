using Api.Dto.Request.CustomerChore.v1;
using Api.Dto.Response.CustomerChore.v1;
using Core.Domain;
using Core.Features.Commands.CustomerChore;
using Core.Features.Queries.CustomerChores;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class CustomerChoreController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerChoreController> _logger;

    public CustomerChoreController(IMediator mediator, IMapper mapper, ILogger<CustomerChoreController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<CustomerChore>>> GetAllChores()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCustomerChoresQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetCustomerChoresByCustomerId/")]
    public async Task<ActionResult<IList<CustomerChoreResponseDto>>> GetCustomerChoresByCustomer([FromQuery] GetCustomerChoresByCustomerIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetCustomerChoresByCustomerIdRequestDto, GetCustomerChoresByCustomerIdQuery>(request));
            return result.Count > 0 ? Ok(_mapper.Map<IList<CustomerChoreResponseDto>>(result)) : new List<CustomerChoreResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in CustomerChore controller: GetCustomerChoresByCustomer");
            return BadRequest(ex.Message);
        }
    }

        [Authorize]
    [HttpGet]
    [Route("GetCustomerChoreById/")]
    public async Task<ActionResult<CustomerChoreResponseDto>> GetCustomerChoreById([FromQuery] GetCustomerChoreByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetCustomerChoreByIdRequestDto, GetCustomerChoreByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<CustomerChoreResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in CustomerChore controller: GetCustomerChoreById");
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CustomerChore>> PostCustomerChoreAsync(PostCustomerChoreRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostCustomerChoreRequestDto, AddCustomerChoreCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<CustomerChore>> PutCustomerChoreAsync(PutCustomerChoreRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PutCustomerChoreRequestDto, UpdateCustomerChoreCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("DeleteCustomerChoreById/")]
    public async Task<ActionResult> DeleteCustomerChoreById([FromQuery] DeleteCustomerChoreByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<DeleteCustomerChoreByIdRequestDto, DeleteCustomerChoreCommand>(request));
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in CustomerChore controller: Could not delete");
            return BadRequest(ex.Message);
        }
    }
}