using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

        var length = (int)stream!.Length;
        var fontData = new byte[length];
        stream.Read(buffer: fontData, 0, length);
        return fontData;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        // Obtém a referência ao assembly atual
        var assembly = Assembly.GetExecutingAssembly();

        // Retorna o stream do arquivo de fonte embutido no assembly
        return assembly.GetManifestResourceStream(
            $"CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts.{faceName}.ttf"
        );
    }
}
