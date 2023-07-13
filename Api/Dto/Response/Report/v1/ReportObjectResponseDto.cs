namespace Api.Dto.Response.Report.v1;

public class ReportObjectResponseDto
{
    public int Id { get; set; }
    public CustomerInfoResponseDto CustomerInfo { get; set; }
    public IssuerInfoResponseDto IssuerInfo { get; set; }
    public IEnumerable<ChoreRowResponseDto> ChoreRows { get; set; }
}