using Core.Features.Queries.Report;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<AuthenticateController> _logger;

    public ReportController(IMapper mapper, IMediator mediator, ILogger<AuthenticateController> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Route("getCustomerReport")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(byte[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerReport([FromBody] string customerId)
    {
        try
        {
            var byteArray = await _mediator.Send(new GetReportQuery { CustomerId = customerId });
            if (byteArray == null)
            {
                return BadRequest();
            }
            return Ok(byteArray);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Report: getCustomerReport");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}