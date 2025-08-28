using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.LoginDto;

namespace Data.Interfaces.IDataImplement.Security
{
    public interface IUserRepository: IData<User> 
    {
        Task<User?> FindByEmailAsync(string emailNorm);
        Task<User?> FindByDocumentAsync(int documentTypeId, string documentNumber);
        Task<bool> VerifyPasswordAsync(User user, string plainPassword);
        Task<User> FindEmail(string email);
    }
}
