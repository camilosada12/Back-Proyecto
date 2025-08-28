using Entity.Domain.Interfaces;

namespace Entity.DTOs.Default.ModelSecurityDto
{
    public class UserDto : IHasId
    {
        public int id { get; set; }
        public string? name { get; set; } // si quieres almacenar alias
        public string? email { get; set; }
        public string? password { get; set; }

        public int? documentTypeId { get; set; }
        public string? documentNumber { get; set; }

        public int PersonId { get; set; }
    }
}
