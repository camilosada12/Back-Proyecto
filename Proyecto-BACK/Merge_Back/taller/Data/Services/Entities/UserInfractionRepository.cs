using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.Infrastructure.Contexts;

namespace Data.Services.Entities
{
    public class UserInfractionRepository : DataGeneric<UserInfraction>, IUserInfractionRepository
    {
        public UserInfractionRepository(ApplicationDbContext context) : base(context) { }
    }
}
