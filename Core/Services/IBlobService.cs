using Core.Domain;

namespace BlobStorage.Services;

public interface IBlobService
{
    public Task<string> UploadBlobAsync(string CustomerChoreId, string FileExtension, Blob Blob);
    public Task<IEnumerable<string>> ListBlobUrlsAsync(string containerName);
}