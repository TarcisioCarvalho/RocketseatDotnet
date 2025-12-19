
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExepensesReadOnlyRepository _exepensesReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    public GenerateExpensesReportPdfUseCase(IExepensesReadOnlyRepository exepensesReadOnlyRepository, ILoggedUser loggedUser)
    {
        _exepensesReadOnlyRepository = exepensesReadOnlyRepository;
        GlobalFontSettings.FontResolver = new Fonts.ExpensesReportFontResolver();
        _loggedUser = loggedUser;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();
        var expenses = await _exepensesReadOnlyRepository.FilterByMonth(loggedUser, month);
        if (expenses.Count == 0)
            return Array.Empty<byte>();
        var document = CreateDocument(loggedUser.Name, month);
        var page = CreatePage(document);
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();


        row.Cells[1].AddParagraph($"Hey, {loggedUser.Name}");
        row.Cells[1].Format.Font = new Font
        {
            Name = FontHelper.RALEWAY_BLACK,
            Size = 16
        };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

        var paragraph = page.AddParagraph();
        paragraph.AddFormattedText($"Total Spend In {month:Y}", new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();
        paragraph.AddFormattedText($"{expenses.Sum(expense => expense.Value)} R$", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
        return RenderDocument(document);
    }

    private Document CreateDocument(string author, DateOnly month)
    {
        var document = new Document();

        document.Info.Title = $"EXPENSES FOR {month:Y}";
        document.Info.Author = author;

        var style = document.Styles["Normal"];
        style.Font.Name = FontHelper.RALEWAY_REGULAR;


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

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }

}
