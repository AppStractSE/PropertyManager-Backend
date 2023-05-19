using Microsoft.AspNetCore.Http;

namespace Core.Domain;

public class UploadBlobRequest
{
    public string CustomerChoreId { get; set; }
    public string FileExtension { get; set; }
    public IFormFile FormFile { get; set; }
    public string? FileName { get; set; }
}