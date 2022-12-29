using Domain.Features.Queries.Customers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.Response.CustomerChore.v1;
using Api.Dto.Request.CustomerChore.v1;
using Domain.Features.Queries.CustomerChore;
using System.Collections.Generic;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class CustomerChoreController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CustomerChoreController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
            return BadRequest(ex.Message);
        }
    }
}