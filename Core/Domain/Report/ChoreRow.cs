using System.Collections.Generic;

namespace Core.Domain.Report;


public class ChoreRow
{
    public string ChoreName { get; set; }
    public IDictionary<int,string> MonthResult { get; set; }
}
