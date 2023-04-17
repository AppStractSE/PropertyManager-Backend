namespace Core.Domain;
public class Blob
{
    public Stream Content { get; set; }
    public string ContentType { get; set; }

    public Blob(Stream _content, string _contentType)
    {
        Content = _content;
        ContentType = _contentType;
    }
}