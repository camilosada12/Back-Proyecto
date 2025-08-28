using Entity.DTOs.Default.Auth.RegisterReponseDto;
using Entity.DTOs.Default.RegisterRequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.BusinessRegister
{
    public interface IUserRegistrationService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto);
    }
}
