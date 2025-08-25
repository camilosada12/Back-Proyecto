using Entity.Domain.Interfaces;

namespace Entity.DTOs.Default.ModelSecurityDto
{
    public class ModuleDto : IHasId
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
