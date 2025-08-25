using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class TypeInfraction : BaseModel
    {
        public string type_Infraction { get; set; }
        public int numer_smldv { get; set; }
        public string description { get; set; }
        
        //relaciones
        public List<UserInfraction> userInfractions { get; set; } = new List<UserInfraction>();
        public List<FineCalculationDetail> fineCalculationDetail { get; set; } = new List<FineCalculationDetail>();
    }
}
