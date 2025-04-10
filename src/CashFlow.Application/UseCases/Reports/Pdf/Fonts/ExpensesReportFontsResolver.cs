using System.Reflection;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Reports.Pdf.Fonts
{
    public class ExpensesReportFontsResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            var stream = ReadFontFile(faceName);

            stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

            var length = stream!.Length;
            var data = new byte[length];
            stream.Read(data, 0, (int)length);

            return data;
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(familyName);
        }

        private Stream? ReadFontFile(string faceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.Reports.Pdf.Fonts.{faceName}.ttf");
        }
    }
}
