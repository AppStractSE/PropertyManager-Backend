namespace Core.Domain.Report;

public class ChoreRow
{
    public string ChoreName { get; set; }
    public IEnumerable<MonthResult> MonthResult { get; set; }
}
