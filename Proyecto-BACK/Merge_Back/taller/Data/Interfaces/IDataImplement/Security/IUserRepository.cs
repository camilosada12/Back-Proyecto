using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.Auth;

namespace Data.Interfaces.IDataImplement.Security
{
    public interface IUserRepository: IData<User> 
    {
        Task<User?> GetByEmailAsync(string email);  // nuevo alias
        Task<User?> FindEmail(string email);        // mantén el tuyo si quieres
        Task<bool> ExistsByEmailAsync(string email);
        //Task<User> ValidateUserAsync(LoginDto loginDto);
        //Task AddAsync(User user);
    }
}
