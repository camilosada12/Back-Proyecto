using Entity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.filter
{
    public class UserInfractionFilterDto
    {
        public int? UserId { get; set; }
        public string? SearchTerm { get; set; }
    }

}
