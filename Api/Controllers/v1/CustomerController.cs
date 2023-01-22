using Domain.Features.Queries.Customers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Domain;
using Api.Dto.Request.Customer.v1;
using Api.Dto.Response.Customer.v1;
using Domain.Features.Commands.Customer;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(IMediator mediator, IMapper mapper, ILogger<CustomerController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Customer>>> GetAllCustomers()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Customer controller: GetAllCustomers");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetCustomerById/")]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomerById([FromQuery]GetCustomerByIdRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetCustomerByIdRequestDto, GetCustomerByIdQuery>(request));
            return result != null ? Ok(_mapper.Map<CustomerResponseDto>(result)) : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in CustomerChore controller: GetCustomerById");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomerAsync(PostCustomerRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostCustomerRequestDto, AddCustomerCommand>(request));
            return Ok(result);    
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}