using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Entities
{
    public class TypeInfractionRepository : DataGeneric<TypeInfraction>, ITypeInfractionRepository
    {
        public TypeInfractionRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Obtener todos los registros activos
        public override async Task<IEnumerable<TypeInfraction>> GetAllAsync()
        {
            return await _dbSet
                .Where(t => t.is_deleted == false)
                .ToListAsync();
        }

        // Obtener registros eliminados (lógica de borrado)
        public override async Task<IEnumerable<TypeInfraction>> GetDeletes()
        {
            return await _dbSet
                .Where(t => t.is_deleted == true)
                .ToListAsync();
        }

        // Obtener un registro por ID
        public override async Task<TypeInfraction?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(t => t.id == id)
                .FirstOrDefaultAsync();
        }
    }
}
