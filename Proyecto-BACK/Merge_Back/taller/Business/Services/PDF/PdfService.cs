using Business.Interfaces.PDF;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Default.EntitiesDto;
using Entity.DTOs.Select.Entities;
using Microsoft.Playwright;
using System.Globalization;
using System.Text.RegularExpressions;
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


        public async Task<byte[]> GeneratePdfAsync(UserInfractionSelectDto dto)
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

        public async Task<byte[]> GeneratePaymentAgreementPdfAsync(PaymentAgreementSelectDto dto)
        {
            var html = BuildPaymentAgreementHtml(dto);

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

        private static string BuildHtml(UserInfractionSelectDto dto)
        {
            var template = InspectoraTemplate.Html; // 👈 CORREGIDO

            return template
         .Replace("@Expediente", HttpUtility.HtmlEncode(dto.id.ToString()))
        .Replace("@Fecha", dto.dateInfraction.ToString("dd 'de' MMMM 'de' yyyy"))
        .Replace("@InfractorNombre", HttpUtility.HtmlEncode($"{dto.firstName} {dto.lastName}"))
        .Replace("@InfractorCedula", HttpUtility.HtmlEncode(dto.documentNumber ?? ""))
        .Replace("@TipoInfraccion", HttpUtility.HtmlEncode(dto.typeInfractionName))
        .Replace("@DescripcionInfraccion", HttpUtility.HtmlEncode(dto.observations));
        }

        private static string BuildPaymentAgreementHtml(PaymentAgreementSelectDto dto)
        {
            var culture = new CultureInfo("es-CO");

            return PaymentAgreementTemplate.Html
                .Replace("@Nombre", HttpUtility.HtmlEncode(dto.PersonName ?? "-"))
                .Replace("@Documento", HttpUtility.HtmlEncode(dto.DocumentNumber ?? "-"))
                .Replace("@TipoDocumento", HttpUtility.HtmlEncode(dto.DocumentType ?? "-"))
                .Replace("@Direccion", HttpUtility.HtmlEncode(dto.address ?? "-"))
                .Replace("@Barrio", HttpUtility.HtmlEncode(dto.Neighborhood ?? "-"))
                .Replace("@Telefono", HttpUtility.HtmlEncode(dto.PhoneNumber ?? "-"))
                .Replace("@Correo", HttpUtility.HtmlEncode(dto.Email ?? "-"))
                .Replace("@FechaInicio", dto.AgreementStart.ToString("dd/MM/yyyy"))
                .Replace("@FechaFin", dto.AgreementEnd.ToString("dd/MM/yyyy"))
                .Replace("@ExpedicionCedula", dto.expeditionCedula.ToString("dd/MM/yyyy"))
                .Replace("@MetodoPago", HttpUtility.HtmlEncode(dto.PaymentMethod ?? "-"))
                .Replace("@FrecuenciaPago", HttpUtility.HtmlEncode(dto.FrequencyPayment ?? "-"))
                .Replace("@TipoInfraccion", HttpUtility.HtmlEncode(dto.TypeFine ?? "-"))
                .Replace("@Infraccion", HttpUtility.HtmlEncode(dto.Infringement ?? "-"))
                .Replace("@Descripcion", HttpUtility.HtmlEncode(dto.Infringement ?? "-"))
                .Replace("@ValorSMDLV", "$ " + dto.ValorSMDLV.ToString("N0", culture))
                .Replace("@MontoBase", "$ " + dto.BaseAmount.ToString("N0", culture))
                .Replace("@Intereses", "$ " + dto.AccruedInterest.ToString("N0", culture))
                .Replace("@SaldoPendiente", "$ " + dto.OutstandingAmount.ToString("N0", culture))
                .Replace("@Cuotas", dto.Installments?.ToString() ?? "-")
                .Replace("@ValorCuota", dto.MonthlyFee.HasValue ? "$ " + dto.MonthlyFee.Value.ToString("N0", culture) : "-")
                .Replace("@Estado", dto.IsPaid ? "✅ Pagado" : "⏳ Pendiente")
                .Replace("@Coactivo", dto.IsCoactive
                    ? $"<p><strong>Proceso coactivo desde:</strong> {dto.CoactiveActivatedOn:dd/MM/yyyy}</p>"
                    : "")
                .Replace("@UltimoInteres", dto.LastInterestAppliedOn.HasValue
                    ? $"<p><strong>Último cálculo de interés:</strong> {dto.LastInterestAppliedOn:dd/MM/yyyy}</p>"
                    : "");
        }
    }
}
