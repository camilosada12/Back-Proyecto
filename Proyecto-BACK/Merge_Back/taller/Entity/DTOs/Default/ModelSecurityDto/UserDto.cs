using Entity.Domain.Interfaces;

namespace Entity.DTOs.Default.ModelSecurityDto
{
    public class UserDto : IHasId
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public int PersonId { get; set; }

    }
}
