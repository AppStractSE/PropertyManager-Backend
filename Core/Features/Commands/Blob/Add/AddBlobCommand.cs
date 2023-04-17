using Core.Domain;
using MediatR;

namespace Core.Features.Commands.BlobStorage;

public class AddBlobCommand : IRequest<string>
{
    public string CustomerChoreId { get; }
    public string FileExtension { get; }
    public Blob Blob { get; }

    public AddBlobCommand(string customerChoreId, string fileExtension, Blob blob)
    {
        CustomerChoreId = customerChoreId;
        FileExtension = fileExtension;
        Blob = blob;
    }
}
