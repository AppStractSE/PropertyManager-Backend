using Core.Features.Queries.Periodics;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class PeriodicController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<PeriodicController> _logger;

    public PeriodicController(IMediator mediator, IMapper mapper, ILogger<PeriodicController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Periodic>>> GetAllPeriodics()
    {
        try
        {
            var result = await _mediator.Send(new GetAllPeriodicsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Periodic controller: GetAllPeriodics");
            return BadRequest(ex.Message);
        }
    }
}