using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class TypeInfractionSelectDto
    {
        public int id { get; set; }
        public string type_Infraction { get; set; }
        public string description { get; set; }
        
    }
}
