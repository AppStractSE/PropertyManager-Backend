using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Dto.Response.City.v1;
using Microsoft.AspNetCore.Authorization;
using Core.Domain;
using Api.Dto.Request.City.v1;
using Core.Features.Commands.City;
using Core.Features.Queries.Cities;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class CityController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private ILogger<CityController> _logger;

    public CityController(IMediator mediator, IMapper mapper, ILogger<CityController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;

    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IList<CityResponseDto>>> GetAllCities()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCitiesQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in City controller: GetAllCities");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<City>> PostCityAsync(PostCityRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<PostCityRequestDto, AddCityCommand>(request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}