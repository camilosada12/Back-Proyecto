// Entity.Domain.Models.Implements.ModelSecurity.User
using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Models.Implements.ModelSecurity
{
    public class User : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string PasswordHash { get; set; } = null!;

        [Column(TypeName = "varchar(150)")]
        public string email { get; set; } = null!; // índice único

        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public List<UserInfraction> UserInfraction { get; set; } = new();
        public List<RolUser> rolUsers { get; set; } = new();
    }
}
