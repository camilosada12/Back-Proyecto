using Entity.Domain.Interfaces;

namespace Entity.DTOs.Default.ModelSecurityDto
{
    public class FormModuleDto : IHasId
    {
        public int id { get; set; }
        public int formid { get; set; }
        public int moduleid { get; set; }
    }
}
