using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class FineCalculationDetail : BaseModel
    {
        public string forumula {  get; set; }
        public decimal percentaje { get; set; }
        public decimal totalCalculation {  get; set; }
        public int valueSmldvId { get; set; }
        public int typeInfractionId { get; set; }

        //Relaciones
        public ValueSmldv valueSmldv { get; set; }
        public TypeInfraction typeInfraction { get; set; }

    }
}
