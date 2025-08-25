using System.ComponentModel.DataAnnotations;
using Entity.Domain.Interfaces;

namespace Entity.DTOs.Default.ModelSecurityDto
{
    public class PersonDto : IHasId
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }

        public int documentTypeId { get; set; }
        public int municipalityId { get; set; }
    }
}
