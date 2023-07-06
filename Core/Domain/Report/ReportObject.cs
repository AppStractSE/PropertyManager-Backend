namespace Core.Domain.Report;

public class ReportObject
{
    public int Id { get; set; }
    public CustomerInfo CustomerInfo { get; set; }
    public IssuerInfo IssuerInfo { get; set; }
    public IEnumerable<ChoreRow> ChoreRows { get; set; }
}