using Entity.Domain.Interfaces;
using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Models.Implements.ModelSecurity
{
    public class User : BaseModel
    {

        [Column(TypeName = "varchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? password { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string email { get; set; }


        // Foreign key
        [ForeignKey("Person")]
        public int? PersonId { get; set; }

        // Navegación hacia Person
        public Person? Person { get; set; }

        public List<UserInfractionDto?> UserInfraction { get; set; } = new List<UserInfractionDto?>();

        public List<RolUser> rolUsers { get; set; } = new();

    }

}
