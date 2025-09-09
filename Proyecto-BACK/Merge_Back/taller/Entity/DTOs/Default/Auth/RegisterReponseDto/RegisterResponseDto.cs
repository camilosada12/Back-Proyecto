using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Default.Auth.RegisterReponseDto
{
    public class RegisterResponseDto
    {
        public bool IsSuccess { get; set; }
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
