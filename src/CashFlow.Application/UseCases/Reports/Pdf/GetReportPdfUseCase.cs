
using CashFlow.Application.UseCases.Reports.Pdf.Fonts;
using CashFlow.Communication.Resources;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Pdf
{
    public class GetReportPdfUseCase : IGetReportPdfUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository;
        public GetReportPdfUseCase(IExpensesReadOnlyRepository repository)
        {
            _repository = repository;

            GlobalFontSettings.FontResolver = new ExpensesReportFontsResolver();
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _repository.FilterByMonth(month);

            if(expenses.Count == 0)
            {
                return [];
            }

            var document = CreateDocument(month);
            var page = CreatePage(document);

            var paragraph = page.AddParagraph();
            paragraph.AddFormattedText(ResourceReportController.TOTAL_SPENT_IN, month.ToString("Y"));
            return [];
        }

        private Document CreateDocument(DateOnly month)
        {
            var document = new Document();

            document.Info.Title = $"{ResourceReportController.TITLE_DOCUMENT}{month.ToString("Y")}";
            document.Info.Author = "João Vitor";

            var style = document.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;

            return document;
        }

        private Section CreatePage(Document document)
        {
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;

            return section;
        }
    }
}
