using Business.Mensajeria.Email.@interface;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Mensajeria.Email.implements
{
    public class ServiceEmail : IServiceEmail
    {
        private readonly IConfiguration _config;
        public ServiceEmail(IConfiguration cfg) => _config = cfg;

        // Ahora en vez de "Bienvenida" tenemos un método específico para códigos
        public async Task EnviarCodigoVerificacion(string emailReceptor, string nombre, string codigo)
        {
            var html = EmailTemplates.CodigoVerificacion(nombre, codigo);
            await EnviarHtmlAsync(emailReceptor, "Código de verificación", html);
        }

        public async Task EnviarHtmlAsync(string to, string subject, string htmlBody)
        {
            // Lee de SmtpSettings
            var host = _config["SmtpSettings:Host"]!;
            var port = int.Parse(_config["SmtpSettings:Port"]!);
            var enableSsl = bool.Parse(_config["SmtpSettings:EnableSsl"]!);
            var fromEmail = _config["SmtpSettings:Email"]!;
            var password = _config["SmtpSettings:Password"]!;

            using var smtp = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            using var msg = new MailMessage
            {
                From = new MailAddress(fromEmail, "Proyecto Hacienda"),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };
            msg.To.Add(to);
            msg.Headers.Add("X-Priority", "3");
            msg.Headers.Add("X-Mailer", "Proyecto-Hacienda-Mailer");

            await smtp.SendMailAsync(msg);
        }
    }

    public static class EmailTemplates
    {
        public static string CodigoVerificacion(string nombre, string codigo)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
  <title>Código de verificación</title>
  <style>
    body{{font-family:Segoe UI,Roboto,Arial,sans-serif;background:#f5f7fb;margin:0;padding:0}}
    .card{{max-width:600px;margin:24px auto;background:#fff;border-radius:12px;
           box-shadow:0 6px 20px rgba(0,0,0,.08);overflow:hidden;padding:28px}}
    h1{{color:#333;font-size:22px}}
    .code{{font-size:32px;font-weight:bold;color:#667eea;letter-spacing:8px;text-align:center;margin:24px 0}}
    .muted{{color:#6b7280;font-size:14px;text-align:center}}
  </style>
</head>
<body>
  <div class='card'>
    <h1>Hola {System.Net.WebUtility.HtmlEncode(nombre)},</h1>
    <p>Para confirmar que tu correo sigue activo, ingresa este código en la aplicación:</p>
    <div class='code'>{codigo}</div>
    <p class='muted'>El código expira en 24 horas.</p>
  </div>
</body>
</html>";
        }
    }
}
