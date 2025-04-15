
using System.Reflection;
using CashFlow.Application.UseCases.Reports.Pdf.Fonts;
using CashFlow.Communication.Resources;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Pdf
{
    public class GetReportPdfUseCase : IGetReportPdfUseCase
    {
        private const string CURRENCY_SIMBOL = "R$";

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

            CreateHeaderWithProfileImageAndName(page);
            var totalExpenses = expenses.Sum(expenses => expenses.Amount);
            CreateTotalExpendSection(page, month, totalExpenses);

            return RenderDocument(document);
        }
        private Document CreateDocument(DateOnly month)
        {
            var document = new Document();

            document.Info.Title = $"{ResourceReportController.TITLE_DOCUMENT}{month:Y}";
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
        private byte[] RenderDocument(Document document) {

            var rederer = new PdfDocumentRenderer()
            {
                Document = document
            };

            rederer.RenderDocument();

            using var stream = new MemoryStream();
            rederer.PdfDocument.Save(stream);

            return stream.ToArray();
        }
        private void CreateHeaderWithProfileImageAndName(Section page)
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn("300");

            var row = table.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var diretoryName = Path.GetDirectoryName(assembly.Location);
            var pathFile = Path.Combine(diretoryName!,"Logo", "logo.png");

            var image = row.Cells[0].AddImage(pathFile);

            image.Width = Unit.FromCentimeter(1.7);
            image.Height = Unit.FromCentimeter(1.7);

            row.Cells[1].AddParagraph("Olá, João!");
            row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        }
        private void CreateTotalExpendSection(Section page, DateOnly month, decimal totalExpenses)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";

            var title = string.Format(ResourceReportController.TOTAL_SPENT_IN, month.ToString("Y"));

            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

            paragraph.AddLineBreak();

            paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SIMBOL}", new Font { Name = FontHelper.RALEWAY_BLACK, Size = 50 });

            paragraph.AddFormattedText();
        }
    }
}
