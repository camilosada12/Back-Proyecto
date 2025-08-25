using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.LoginDto;

namespace Data.Interfaces.IDataImplement.Security
{
    public interface IUserRepository: IData<User> 
    {
        Task<User> ValidateUserAsync(LoginDto loginDto);
        Task<User?> FindEmail(string email);
    }
}
