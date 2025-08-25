using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class ValueSmldv : BaseModel
    {
        public double value_smldv { get; set; }
        public int Current_Year { get; set; }
        public decimal minimunWage { get; set; }

        //Relaciones
        public List<FineCalculationDetail> FineCalculationDetail { get; set; } = new List<FineCalculationDetail>();
    }
}
