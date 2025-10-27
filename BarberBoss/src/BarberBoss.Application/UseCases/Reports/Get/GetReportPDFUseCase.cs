using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Repositories;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text;

namespace BarberBoss.Application.UseCases.Reports.Get;
public class GetReportPDFUseCase : IGetReportPDFUseCase
{
    private readonly IBillingReadOnlyRepository _billingReadOnlyRepository;
    public GetReportPDFUseCase(IBillingReadOnlyRepository billingReadOnlyRepository)
    {
        _billingReadOnlyRepository = billingReadOnlyRepository;
    }
    public async Task<byte[]> Execute(RequestReportJson request)
    {
        var billingsToReport = await _billingReadOnlyRepository.GetBillingReport(request.StartDate, request.EndDate);

        // Calcular total
        var total = billingsToReport.Sum(b => b.Amount);

        // Gerar HTML
        var html = GenerateHtml(billingsToReport, total, request.StartDate, request.EndDate);

        // Baixar Chromium (só na primeira vez!)
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        // Abrir navegador headless
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });

        // Nova página
        await using var page = await browser.NewPageAsync();

        // Carregar HTML
        await page.SetContentAsync(html);

        // Gerar PDF
        var pdfBytes = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true,
            MarginOptions = new MarginOptions
            {
                Top = "20px",
                Bottom = "20px",
                Left = "20px",
                Right = "20px"
            }
        });

        return pdfBytes;
    }
    private string GenerateHtml(IEnumerable<dynamic> billings, decimal total, DateTime? startDate, DateTime? endDate)
    {
        var html = new StringBuilder();
        var logoBase64 = GetLogoBase64();
        html.Append(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        
        body {
            font-family: Arial, sans-serif;
            padding: 40px;
        }
        
        .header {
            display: flex;
            align-items: center;
            margin-bottom: 30px;
        }
        
        .logo {
            width: 80px;
            height: 80px;
            background: #1e5631;
            border-radius: 50%;
            margin-right: 20px;
        }
        
        .title {
            font-size: 28px;
            font-weight: bold;
        }
        
        .period {
            color: #666;
            margin-bottom: 10px;
        }
        
        .total-section {
            margin-bottom: 30px;
        }
        
        .total-label {
            font-size: 14px;
            color: #666;
            margin-bottom: 5px;
        }
        
        .total-value {
            font-size: 36px;
            font-weight: bold;
        }
        
        .service-section {
            margin-bottom: 30px;
        }
        
        .service-header {
            background: #1e5631;
            color: white;
            padding: 10px 15px;
            font-weight: bold;
            margin-bottom: 10px;
        }
        
        .billing-item {
            background: #f5f5f5;
            padding: 15px;
            margin-bottom: 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        
        .billing-info {
            flex: 1;
        }
        
        .billing-date {
            font-size: 14px;
            color: #666;
        }
        
        .billing-payment {
            font-size: 14px;
            color: #666;
            margin-top: 5px;
        }
        
        .billing-value {
            font-size: 18px;
            font-weight: bold;
        }
        
        .billing-note {
            font-size: 12px;
            color: #999;
            margin-top: 5px;
            font-style: italic;
        }
    </style>
</head>
<body>
    <div class='header'>");


        if (!string.IsNullOrEmpty(logoBase64))
        {
            html.Append($@"
        <img src='data:image/png;base64,{logoBase64}' 
             style='width: 80px; height: 80px; border-radius: 50%; margin-right: 20px;' />");
        }

        html.Append(@"
        <div class='title'>BARBEARIA DO JOÃO</div>
    </div>");

        // Período
        if (startDate.HasValue && endDate.HasValue)
        {
            html.Append($@"
    <div class='period'>
        Período: {startDate.Value:dd/MM/yyyy} a {endDate.Value:dd/MM/yyyy}
    </div>");
        }

        // Total
        html.Append($@"
    <div class='total-section'>
        <div class='total-label'>Faturamento do período</div>
        <div class='total-value'>R$ {total:N2}</div>
    </div>");

        // Agrupar por serviço
        var groupedBillings = billings.GroupBy(b => b.ServiceName);

        foreach (var group in groupedBillings)
        {
            html.Append($@"
    <div class='service-section'>
        <div class='service-header'>{group.Key.ToUpper()}</div>");

            foreach (var billing in group)
            {
                html.Append($@"
        <div class='billing-item'>
            <div class='billing-info'>
                <div class='billing-date'>{billing.Date:dd/MM/yyyy}</div>
                <div class='billing-payment'>{FormatPaymentMethod(billing.PaymentMethod)}</div>");

                if (!string.IsNullOrEmpty(billing.Notes))
                {
                    html.Append($@"
                <div class='billing-note'>{billing.Notes}</div>");
                }

                html.Append(@"
            </div>
            <div class='billing-value'>R$ " + $"{billing.Amount:N2}" + @"</div>
        </div>");
            }

            html.Append(@"
    </div>");
        }

        html.Append(@"
</body>
</html>");

        return html.ToString();
    }

    private string FormatPaymentMethod(PaymentMethod method)
    {
        return method switch
        {
            PaymentMethod.Credit_Card => "Cartão de crédito",
            PaymentMethod.Cash => "Dinheiro",
            PaymentMethod.Pix => "Pix",
            PaymentMethod.Other => "Outro",
            _ => method.ToString()
        };
    }
    private string GetLogoBase64()
    {
        try
        {
            var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "barber-boss-logo.png");

            if (!File.Exists(logoPath))
                return string.Empty;

            var imageBytes = File.ReadAllBytes(logoPath);
            return Convert.ToBase64String(imageBytes);
        }
        catch
        {
            return string.Empty;
        }
    }
}
