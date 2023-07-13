using Core.Domain.Report;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Core.Features.Queries.Report;

public class GetExcelReportQueryHandler : IRequestHandler<GetExcelReportQuery, byte[]>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IArchivedChoreStatusRepository _archRepo;

    public GetExcelReportQueryHandler(IMapper mapper, IMediator mediator, IArchivedChoreStatusRepository archRepo)
    {
        _archRepo = archRepo;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<byte[]> Handle(GetExcelReportQuery request, CancellationToken cancellationToken)
    {
       var reportObject = await _mediator.Send(_mapper.Map<GetExcelReportQuery, GetReportQuery>(request));

       return await GenerateXLSAsync(reportObject);
    }

    private static async Task<byte[]> GenerateXLSAsync(ReportObject data)
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

            infoHeaderStyle.BorderTop = BorderStyle.None;
            infoHeaderStyle.BorderBottom = BorderStyle.None;
            infoHeaderStyle.BorderLeft = BorderStyle.None;
            infoHeaderStyle.BorderRight = BorderStyle.None;

            infoHeaderStyle.Alignment = HorizontalAlignment.Left;
            var infoHeaderFont = workbook.CreateFont();
            infoHeaderFont.IsBold = true;
            infoHeaderFont.FontHeightInPoints = 14;
            infoHeaderStyle.SetFont(infoHeaderFont);

            //STYLES END


            // Issuer and customer information
            IRow informationRow = sheet.CreateRow(0);
            informationRow.RowStyle = infoHeaderStyle;

            var issuerCell = informationRow.CreateCell(0);
            issuerCell.SetCellValue("Issuer:");
            issuerCell.CellStyle = infoHeaderStyle;

            var customerCell = informationRow.CreateCell(6);
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

            using var stream = new MemoryStream();
            workbook.Write(stream, false);
            return await Task.FromResult(stream.ToArray());
        }
    }
}
