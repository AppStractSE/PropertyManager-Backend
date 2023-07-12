using Core.Domain;
using Core.Domain.Report;
using Core.Features.Queries.CustomerChores;
using Core.Features.Queries.Customers;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Globalization;

namespace Core.Features.Queries.Report;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, byte[]>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IArchivedChoreStatusRepository _archRepo;

    public GetReportQueryHandler(IMapper mapper, IMediator mediator, IArchivedChoreStatusRepository archRepo)
    {
        _archRepo = archRepo;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<byte[]> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var reportObject = await GenerateReportObjectAsync(request.CustomerId, cancellationToken);

        var xlsReport = await GenerateXLSAsync(reportObject);

        // TODO:
        // 1. Create a new instance of the report (X)
        // 2. Generate the report object (X)
        // 3. Generate the report in xls (X)
        // 4. Return the report as a byte array (/)
        // 5. Add a new endpoint in the controller to return the report as a file (/)
        // Done!

        return await Task.FromResult(new byte[0]);
    }

    private async Task<byte[]> GenerateXLSAsync(ReportObject data)
    {
        using (IWorkbook workbook = new XSSFWorkbook())
        {
            ISheet sheet = workbook.CreateSheet("Report");

            //STYLES START

            // Create a style for the table headers
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.Alignment = HorizontalAlignment.Center;
            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);

            //Create a style for data cells
            var dataStyle = workbook.CreateCellStyle();
            dataStyle.Alignment = HorizontalAlignment.Center;
            var dataFont = workbook.CreateFont();
            dataFont.IsBold = false;
            dataStyle.SetFont(dataFont);

            //Create a style for the information header
            var infoHeaderStyle = workbook.CreateCellStyle();
            infoHeaderStyle.Alignment = HorizontalAlignment.Left;
            var infoHeaderFont = workbook.CreateFont();
            infoHeaderFont.IsBold = true;
            infoHeaderFont.FontHeightInPoints = 14;
            infoHeaderStyle.SetFont(infoHeaderFont);

            //STYLES END


            // Issuer and customer information
            IRow issuerRow = sheet.CreateRow(0);
            var issuerCell = issuerRow.CreateCell(0);
            issuerCell.SetCellValue("Issuer:");
            issuerCell.CellStyle = infoHeaderStyle;

            var customerCell = issuerRow.CreateCell(6);
            customerCell.SetCellValue("Customer:");
            customerCell.CellStyle = infoHeaderStyle;

            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 5)); // Merge cells for Issuer
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 6, 11)); // Merge cells for Customer


            string[] issuerData = { data.IssuerInfo.Name, data.IssuerInfo.Address, data.IssuerInfo.Email, data.IssuerInfo.PhoneNumber };
            string[] customerData = { data.CustomerInfo.Name, data.CustomerInfo.Address, "{CustomerEmail}", "{CustomerPhoneNumber}" };

            for (int i = 0; i < issuerData.Length; i++)
            {
                var row = sheet.CreateRow(i + 1);

                //Issuer data
                var cell = row.CreateCell(0);
                cell.SetCellValue(issuerData[i]);
                sheet.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 0, 5)); // Merge cells

                //Customer data
                cell = row.CreateCell(6);
                cell.SetCellValue(customerData[i]);
                sheet.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 6, 11)); // Merge cells
            }

            // Create the table header
            var tableHeaderRow = sheet.CreateRow(6);
            var title = tableHeaderRow.CreateCell(0);
            title.SetCellValue("Titel");
            title.CellStyle = headerStyle; // Apply header style

            string[] months = { "Jan", "Feb", "Mar", "Apr", "Maj", "Jun", "Jul", "Aug", "Sep", "Nov", "Dec" };
            for (int i = 0; i < months.Length; i++)
            {
                var cell = tableHeaderRow.CreateCell(i + 1);
                cell.SetCellValue(months[i]);
                cell.CellStyle = headerStyle; // Apply header style
            }

            // Create rows for the chores
            for (int i = 0; i < data.ChoreRows.Count(); i++)
            {
                var row = sheet.CreateRow(i + 7);
                var chore = data.ChoreRows.ToList()[i];
                row.CreateCell(0).SetCellValue(chore.ChoreName);

                sheet.AutoSizeColumn(0);
                foreach (var month in chore.MonthResult)
                {
                    var cell = row.CreateCell(month.MonthNr);
                    cell.SetCellValue(month.Progress);
                    cell.CellStyle = dataStyle;
                }
            }

            FileStream sw = File.Create("test.xlsx");
            workbook.Write(sw, false);
            sw.Close();
        }

        return await Task.FromResult(new byte[0]);
    }

    private async Task<ReportObject> GenerateReportObjectAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var dateNow = DateTime.Now;
        var choreRows = new List<ChoreRow>();
        var customerInfo = _mapper.Map<CustomerInfo>(await _mediator.Send(new GetCustomerByIdQuery { Id = customerId }));
        var issuerInfo = new IssuerInfo
        {
            Id = new Guid(),
            Name = "HSB Norra Götaland",
            Address = "Property Manager Street 1",
            Email = "ng@hsb.se",
            PhoneNumber = "0707123456"
        };


        var customerChores = await _mediator.Send(new GetCustomerChoresByCustomerIdQuery { Id = customerId }, cancellationToken);

        foreach (var customerChore in customerChores)
        {
            var allAchivedChores = await _archRepo.GetQuery(x => x.CustomerChoreId == customerChore.Id.ToString());
            var monthResult = new List<MonthResult>();


            for (var i = 1; i <= dateNow.Month; i++)
            {
                var monthNr = i;
                var thisMonthResult = new MonthResult
                {
                    MonthNr = monthNr,
                    Progress = "-"
                };

                var achivedChoreStatuses = allAchivedChores.Where(x => x.CompletedDate.Month == monthNr && x.CompletedDate.Year == dateNow.Year);
                decimal progress = achivedChoreStatuses.Count() / customerChore.Frequency;
                var test = CheckStatusOK(achivedChoreStatuses.Count(), customerChore.Frequency, customerChore.Periodic, monthNr);

                Console.WriteLine("Test: " + test);

                if (progress == 0m)
                {
                    thisMonthResult.Progress = "-";
                }

                if (progress > 0m && progress < 1m)
                {
                    thisMonthResult.Progress = $"{achivedChoreStatuses.Count()} / {customerChore.Frequency}";
                }

                if (progress >= 1m)
                {
                    thisMonthResult.Progress = "OK";
                }
                monthResult.Add(thisMonthResult);
            }

            var choreRow = new ChoreRow
            {
                ChoreName = customerChore.Chore.Title,
                MonthResult = monthResult
            };

            choreRows.Add(choreRow);
        }

        return new ReportObject
        {
            Id = new Random().Next(0, 9999999),
            CustomerInfo = customerInfo,
            IssuerInfo = issuerInfo,
            ChoreRows = choreRows
        };

    }

    private static decimal CheckStatusOK(int choresDone, int frequency, Periodic periodic, int monthNr)
    {
        var year = DateTime.Now.Year;

        var calendar = new GregorianCalendar();
        int weeksInMonth = calendar.GetWeekOfYear(new DateTime(year, monthNr, 1), CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        int daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, monthNr))
            .Select(day => new DateTime(year, monthNr, day))
            .Count(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);

        Console.WriteLine(periodic.Name);
        return periodic.Id.ToString() switch
        {
            // Daily
            "6015CC43-A50F-4415-A369-08DB578342EB" => choresDone / (frequency * daysInMonth),
            // Weekly
            "90B6F72A-DCF8-4301-A36A-08DB578342EB" => (choresDone / frequency) * weeksInMonth,
            // Monthly
            "AE0C808E-1A8A-45E7-A36B-08DB578342EB" => choresDone / frequency,
            // Yearly
            "AD68415B-1E99-4210-A36C-08DB578342EB" => choresDone / frequency,
            _ => (decimal)666,
        };
    }
}
