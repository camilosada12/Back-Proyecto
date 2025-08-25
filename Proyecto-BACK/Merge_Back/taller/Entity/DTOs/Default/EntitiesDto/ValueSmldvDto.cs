using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;

namespace Entity.Domain.Models.Implements.Entities
{
    public class ValueSmldvDto : IHasId
    {
        public int id { get; set; }
        public int value_smldv { get; set; }
        public DateTime Current_Year { get; set; } = DateTime.Now;
        public decimal minimunWage { get; set; }
    }
}
