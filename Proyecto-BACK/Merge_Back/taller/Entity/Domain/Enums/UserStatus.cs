using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Enums
{
    public enum UserStatus
    {
        Pending,   // Usuario recién registrado, esperando validar
        Active,    // Usuario verificado
        Blocked    // Usuario bloqueado por no confirmar en la verificación mensual
    }
}
