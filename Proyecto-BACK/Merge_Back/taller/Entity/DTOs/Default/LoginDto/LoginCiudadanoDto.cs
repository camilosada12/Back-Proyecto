using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Default.LoginDto
{
    public class LoginCiudadanoDto
    {
        public int? TipoDocId { get; set; }          // debe venir el ID del tipo de documento
        public string NumeroDoc { get; set; } = null!;
    }
}
