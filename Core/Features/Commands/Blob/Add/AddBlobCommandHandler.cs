using BlobStorage.Services;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.BlobStorage;

public class AddBlobCommandHandler : IRequestHandler<AddBlobCommand, string>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IBlobService _blobService;
    public AddBlobCommandHandler(IChoreRepository repo, IMapper mapper, ICache redisCache, IBlobService blobService)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = redisCache;
        _blobService = blobService;
    }
    public async Task<string> Handle(AddBlobCommand request, CancellationToken cancellationToken)
    {
        return await _blobService.UploadBlobAsync(request.CustomerChoreId, request.FileExtension, request.Blob);
    }
}