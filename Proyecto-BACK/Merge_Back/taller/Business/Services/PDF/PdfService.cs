using Business.Interfaces.PDF;
using Entity.DTOs.Default.EntitiesDto;
using Microsoft.Playwright;
using System.Web;
using Template.Templates;

namespace Business.Services.PDF
{
    public class PdfService : IPdfGeneratorService
    {
        private static IPlaywright? _playwright;
        private static IBrowser? _browser;

        private async Task EnsureBrowserAsync()
        {
            if (_playwright == null)
                _playwright = await Playwright.CreateAsync();

            if (_browser == null)
                _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true
                });
        }

        public async Task<byte[]> GeneratePdfAsync(InspectoraPdfDto dto)
        {
            var html = BuildHtml(dto);

            await EnsureBrowserAsync();

            var context = await _browser!.NewContextAsync(new() { ViewportSize = null });
            try
            {
                var page = await context.NewPageAsync();
                await page.EmulateMediaAsync(new() { Media = Media.Print });
                await page.SetContentAsync(html, new PageSetContentOptions
                {
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 10_000
                });

                return await page.PdfAsync(new PagePdfOptions
                {
                    Format = "A4",
                    PrintBackground = true,
                    Margin = new()
                    {
                        Top = "40px",
                        Bottom = "40px",
                        Left = "40px",
                        Right = "40px"
                    }
                });
            }
            finally
            {
                await context.CloseAsync();
            }
        }

        private static string BuildHtml(InspectoraPdfDto dto)
        {
            var template = InspectoraTemplate.Html; // 👈 CORREGIDO

            return template
                .Replace("@Expediente", HttpUtility.HtmlEncode(dto.Expediente))
                .Replace("@Fecha", dto.Fecha.ToString("dd 'de' MMMM 'de' yyyy"))
                .Replace("@InfractorNombre", HttpUtility.HtmlEncode(dto.InfractorNombre))
                .Replace("@InfractorCedula", HttpUtility.HtmlEncode(dto.InfractorCedula))
                .Replace("@TipoInfraccion", HttpUtility.HtmlEncode(dto.TipoInfraccion))
                .Replace("@DescripcionInfraccion", HttpUtility.HtmlEncode(dto.DescripcionInfraccion))
                .Replace("@Articulo", HttpUtility.HtmlEncode(dto.Articulo))
                .Replace("@SalariosMinimos", dto.SalariosMinimos.ToString())
                .Replace("@ValorMultaPesos", dto.ValorMultaPesos.ToString("C"))
                .Replace("@Asunto", HttpUtility.HtmlEncode(dto.Asunto))
                .Replace("@Mensaje", HttpUtility.HtmlEncode(dto.Mensaje));
        }
    }
}
