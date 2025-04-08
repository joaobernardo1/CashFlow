using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Reports.Excel
{
    public class GetReportExcelUseCase : IGetReportExcelUseCase
    {
        public Task<byte[]> Execute(DateOnly month)
        {
            var workbook = new XLWorkbook();

            workbook.Author = "CashFlow";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Times New Roman";

            var worksheet = workbook.AddWorksheet(month.ToString("Y"));

        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = "Titulo";
        }
    }
}
