using Microsoft.AspNetCore.Mvc;
using BlobStorage.Services;
using Core.Domain;
using MapsterMapper;
using MediatR;
using Core.Features.Commands.BlobStorage;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class BlobController : ControllerBase
{
    private readonly IBlobService _blobService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;


    public BlobController(IBlobService blobService, IMapper mapper, IMediator mediator)
    {
        _blobService = blobService;
        _mapper = mapper;
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UploadBlob([FromForm] UploadBlobRequest request)
    {
        try
        {
            var blob = new Blob(request.FormFile.OpenReadStream(), request.FormFile.ContentType);
            return Ok(await _mediator.Send(new AddBlobCommand(request.CustomerChoreId, request.FileExtension, blob)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/{containerName}")]
    public async Task<ActionResult<IList<string>>> ListBlobs(string containerName)
    {
        try
        {
            var blobInfos = await _blobService.ListBlobUrlsAsync(containerName);
            return Ok(blobInfos);
        }
        catch (Exception)
        {
            return Ok(new List<string>());
        }
    }

}