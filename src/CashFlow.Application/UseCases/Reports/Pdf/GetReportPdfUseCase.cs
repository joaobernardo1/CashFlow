
using System.Reflection;
using CashFlow.Application.UseCases.Reports.Pdf.Colors;
using CashFlow.Application.UseCases.Reports.Pdf.Fonts;
using CashFlow.Communication.Resources;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
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

            foreach (var expense in expenses)
            {
                var table = CreateTable(page);
                CreateFirstLine(table, expense);
                CreateSecondLine(table, expense);
                CreateDescriptionLine(table, expense);
                var row = table.AddRow();
                row.Borders.Visible = false;
            }

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

            paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SIMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });

            paragraph.AddFormattedText();
        }
        private Table CreateTable(Section page)
        {
            var table = page.AddTable();

            table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return table;
        }
        private void CreateFirstLine(Table table, Expense expense)
        {
            var row = table.AddRow();
            row.Height = 25;
            row.Cells[0].AddParagraph(expense.Title);
            row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.WHITE};
            row.Cells[0].Shading.Color = ColorHelper.RED_DARK;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[3].AddParagraph("Amount");
            row.Cells[3].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.WHITE };
            row.Cells[3].Shading.Color = ColorHelper.RED_DARK;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        }
        private void CreateSecondLine(Table table, Expense expenses)
        {
            var row = table.AddRow();
            row.Height = 25;
            row.Cells[0].AddParagraph(expenses.Time.ToString("D"));
            SetStyleForExpenseInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(expenses.Time.ToString("t"));
            SetStyleForExpenseInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(expenses.PaymentType.PaymentTypeToString());
            SetStyleForExpenseInformation(row.Cells[2]);

            row.Cells[3].AddParagraph($"-{expenses.Amount.ToString()}{CURRENCY_SIMBOL}");
            SetStyleForExpenseInformation(row.Cells[3]);

        }

        private void CreateDescriptionLine(Table table, Expense expense)
        {
            var description = expense.Description;
            if(description != null)
            {
                var row = table.AddRow();
                row.Height = 25;
                row.Cells[0].AddParagraph(description);
                SetStyleForExpenseInformation(row.Cells[0]);
                row.Cells[0].Format.LeftIndent = 20;
                row.Cells[0].MergeRight = 3;
            }

        }
        private void SetStyleForExpenseInformation(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorHelper.BLACK };
            cell.Shading.Color = ColorHelper.GREEN_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
