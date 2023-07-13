using Api.Dto.Requests.Report.v1;
using Api.Dto.Response.Report.v1;
using Core.Domain.Report;
using Core.Features.Queries.Report;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("getCustomerReport")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportObjectResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerReport([FromQuery] GetReportObjectRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetReportObjectRequestDto, GetReportQuery>(request));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<ReportObject, ReportObjectResponseDto>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Report: getCustomerReport");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("getReportExcel")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExcelReport([FromQuery] GetExcelReportRequestDto request)
    {
        try
        {
            var result = await _mediator.Send(_mapper.Map<GetExcelReportRequestDto, GetExcelReportQuery>(request));
            if (result == null)
            {
                return BadRequest();
            }

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "Error in Report: getCustomerReport");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}