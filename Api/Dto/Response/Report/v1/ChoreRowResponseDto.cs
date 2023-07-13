namespace Api.Dto.Response.Report.v1;

public class ChoreRowResponseDto
{
    public string ChoreName { get; set; }
    public IEnumerable<MonthResultResponseDto> MonthResult { get; set; }
}
