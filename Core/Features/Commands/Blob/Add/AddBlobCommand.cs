using Core.Domain;
using MediatR;

namespace Core.Features.Commands.BlobStorage;

public class AddBlobCommand : IRequest<string>
{
    public string CustomerChoreId { get; }
    public string FileExtension { get; }
    public Blob Blob { get; }
    public string? FileName { get; }

    public AddBlobCommand(string customerChoreId, string fileExtension, Blob blob, string? fileName)
    {
        CustomerChoreId = customerChoreId;
        FileExtension = fileExtension;
        Blob = blob;
        FileName = fileName;
    }
}
