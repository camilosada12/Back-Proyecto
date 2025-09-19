using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.implements
{
    public class VerificacionEmailBuilder : IEmailContentBuilder
    {
        private readonly string _nombre;
        private readonly string _codigo;

        public VerificacionEmailBuilder(string nombre, string codigo)
        {
            _nombre = nombre;
            _codigo = codigo;
        }

        public string GetSubject() => "Código de verificación";

        public string GetBody() =>
            $"<p>Hola {_nombre},</p>" +
            $"<p>Tu código de verificación es: <b>{_codigo}</b></p>" +
            "<p>Este código expira en 15 minutos.</p>";

        public IEnumerable<Attachment>? GetAttachments()
        {
            return null; // No hay adjuntos
        }
    }
}
