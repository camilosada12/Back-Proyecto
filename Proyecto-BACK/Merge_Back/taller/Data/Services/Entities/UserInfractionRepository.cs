using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.filter;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Entities
{
    public class UserInfractionRepository : DataGeneric<UserInfraction>, IUserInfractionRepository
    {
        public UserInfractionRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<UserInfraction>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.typeInfraction)
                .Include(u => u.User)
                 .ThenInclude(ui => ui.Person)
                .Where(u => u.is_deleted == false)
                .ToListAsync();
        }

        public override async Task<IEnumerable<UserInfraction>> GetDeletes()
        {
            return await _dbSet
                 .Include(u => u.typeInfraction)
                .Include(u => u.User)
                 .ThenInclude(ui => ui.Person)
                .Where(u => u.is_deleted == true)
                .ToListAsync();
        }

        public override async Task<UserInfraction?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.typeInfraction)
                .Include(u => u.User)
                 .ThenInclude(ui => ui.Person)
                .FirstOrDefaultAsync(u => u.id == id);  
        }

        public async Task<IReadOnlyList<UserInfraction>> GetByDocumentAsync(int documentTypeId, string documentNumber)
        {
            documentNumber = documentNumber.Trim();

            return await _dbSet
                .AsNoTracking()
                .Include(u => u.typeInfraction)
                .Include(u => u.User)
                    .ThenInclude(ui => ui.Person)
                .Where(u => !u.is_deleted &&
                            u.User.documentTypeId == documentTypeId &&
                            u.User.documentNumber == documentNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserInfraction>> FilterAsync(UserInfractionFilterDto filter)
        {
            var query = _dbSet
                .Include(u => u.typeInfraction)
                .Include(u => u.User).ThenInclude(ui => ui.Person)
                .Where(u => !u.is_deleted)
                .AsQueryable();

            if (filter.UserId.HasValue)
                query = query.Where(u => u.UserId == filter.UserId.Value);




            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = $"%{filter.SearchTerm.ToLower()}%";

                query = query.Where(u =>
                    EF.Functions.Like((u.typeInfraction.type_Infraction ?? "").ToLower(), term) ||
                    EF.Functions.Like((u.observations ?? "").ToLower(), term) ||
                    EF.Functions.Like((u.User.Person.firstName ?? "").ToLower(), term) ||
                    EF.Functions.Like((u.User.Person.lastName ?? "").ToLower(), term) ||
                    EF.Functions.Like((u.User.documentNumber ?? ""), term)
                );
            }
            return await query.ToListAsync();
        }


    }
}
