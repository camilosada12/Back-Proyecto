using Entity.Domain.Models.Base;
using Entity.Domain.Models.Implements.parameters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Models.Implements.ModelSecurity
{
    public class Person : BaseModel
    {

        [Column(TypeName = "varchar(100)")]
        public string? firstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? lastName { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? phoneNumber { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? address { get; set; }

        public int documentTypeId { get; set; }       
        public documentType documentType { get; set; }

        public int municipalityId { get; set; }        
        public municipality municipality { get; set; }

        // Navegación hacia User
        public User? User { get; set; }
    }

}
