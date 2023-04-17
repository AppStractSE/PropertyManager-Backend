
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Domain;

namespace BlobStorage.Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadBlobAsync(string customerChoreId, string FileExtension, Blob blob)
    {
        var validContainerName = Regex.Replace(customerChoreId.ToLower(), @"[^a-z0-9\-]", string.Empty);
        var containerClient = _blobServiceClient.GetBlobContainerClient(validContainerName);
        if (!await containerClient.ExistsAsync()) await containerClient.CreateAsync(PublicAccessType.Blob);

        var fileName = Guid.NewGuid().ToString();
        var blobClient = containerClient.GetBlobClient(fileName);
        await containerClient.UploadBlobAsync($"{fileName}.{FileExtension}", blob.Content);

        return blobClient.Uri.ToString();
    }


    public async Task<IEnumerable<string>> ListBlobUrlsAsync(string containerName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobUrls = new List<string>();

        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            var blobClient = containerClient.GetBlobClient(blobItem.Name);
            var blobProperties = await blobClient.GetPropertiesAsync();
            var contentType = blobProperties.Value.ContentType;
            blobUrls.Add(blobClient.Uri.ToString());
        }

        return blobUrls;
    }



}