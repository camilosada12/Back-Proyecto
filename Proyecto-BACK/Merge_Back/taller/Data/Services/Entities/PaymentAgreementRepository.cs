using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Services.Entities
{
    public class PaymentAgreementRepository : DataGeneric<PaymentAgreement>, IPaymentAgreementRepository
    {
        public PaymentAgreementRepository(ApplicationDbContext context) : base(context) 
        {
        }

        public override async Task<IEnumerable<PaymentAgreement>> GetAllAsync()
        {
            return await _dbSet
                        .Include(p => p.userInfraction)
                        .Include(p => p.paymentFrequency)
                        .Where(u => u.is_deleted == false)
                        .ToListAsync();
        }

        public override async Task<IEnumerable<PaymentAgreement>> GetDeletes()
        {
            return await _dbSet
                        .Include(p => p.userInfraction)
                        .Include(p => p.paymentFrequency)
                        .Where(p => p.is_deleted == true)
                        .ToListAsync();
        }

        public override async Task<PaymentAgreement?> GetByIdAsync(int id)
        {
            return await _dbSet
                      .Include(p => p.userInfraction)
                      .Include(p => p.paymentFrequency)
                      .Where(p => p.id == id)
                      .FirstOrDefaultAsync();

        }
    }
}
