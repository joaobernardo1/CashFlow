using CashFlow.Domain.Enums;
using CashFlow.Communication.Resources;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;
using CashFlow.Domain.Extensions;

namespace CashFlow.Application.UseCases.Reports.Excel
{
    public class GetReportExcelUseCase : IGetReportExcelUseCase
    {
        private const string CURRENCY_SIMBOL = "R$";
        private readonly IExpensesReadOnlyRepository _repository;

        public GetReportExcelUseCase(IExpensesReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _repository.FilterByMonth(month);

            if(expenses.Count == 0)
            {
                return [];
            }

           using var workbook = new XLWorkbook();

            workbook.Author = "CashFlow";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Times New Roman";


            var worksheet = workbook.AddWorksheet(month.ToString("Y"));

            InsertHeader(worksheet);

            var raw = 2;

            foreach(var expense in expenses)
            {

                worksheet.Cell($"A{raw}").Value = expense.Title;
                worksheet.Cell($"B{raw}").Value = expense.Time.ToString("Y");
                worksheet.Cell($"C{raw}").Value = expense.Description;
                worksheet.Cell($"D{raw}").Value = expense.PaymentType.PaymentTypeToString();
                worksheet.Cell($"E{raw}").Value = expense.Amount;
                worksheet.Cell($"E{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SIMBOL} #,##0.00";
                
                raw++;
            }

            worksheet.Columns().AdjustToContents();

            var file = new MemoryStream();

            workbook.SaveAs(file);

            return file.ToArray();
        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportController.TITLE;
            worksheet.Cell("B1").Value = ResourceReportController.DATE;
            worksheet.Cell("C1").Value = ResourceReportController.DESCRIPTION;
            worksheet.Cell("D1").Value = ResourceReportController.PAYMENT_TYPE;
            worksheet.Cell("E1").Value = ResourceReportController.AMOUNT;

            worksheet.Cells("A1:E1").Style
                .Font.SetBold()
                .Fill.BackgroundColor = XLColor.FromHtml("#F4A460");

            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

        }
    }
}
