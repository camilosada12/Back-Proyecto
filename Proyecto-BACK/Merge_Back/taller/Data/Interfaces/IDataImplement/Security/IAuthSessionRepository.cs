using Entity.Domain.Models.Implements.ModelSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IDataImplement.Security
{
    // Data/Interfaces/IDataImplement/Security/IAuthSessionRepository.cs
    public interface IAuthSessionRepository
    {
        Task CreateAsync(AuthSession s);
        Task<AuthSession?> GetAsync(int sessionId);
        Task TouchAsync(int sessionId, DateTime now);
        Task RevokeAsync(int sessionId);
    }

}
