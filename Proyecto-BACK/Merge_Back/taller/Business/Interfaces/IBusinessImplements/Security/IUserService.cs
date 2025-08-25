using Business.Interfaces.BusinessBasic;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;

namespace Business.Interfaces.IBusinessImplements.Security
{
    public interface IUserService : IBusiness<UserDto, UserSelectDto>
    {
        Task<UserDto> CreateAsyncUser(UserDto dto);
        Task<User> createUserGoogle(string email, string name);
    }
}
