using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.Infrastructure.Contexts;
using Entity.Init;
using Microsoft.EntityFrameworkCore;

public class PaymentAgreementRepository : DataGeneric<PaymentAgreement>, IPaymentAgreementRepository
{
    public PaymentAgreementRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<IEnumerable<PaymentAgreement>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.userInfraction)
                .ThenInclude(ui => ui.User)
                    .ThenInclude(u => u.Person)
            .Include(p => p.userInfraction.User.documentType)
            .Include(p => p.userInfraction.typeInfraction)
                .ThenInclude(ti => ti.fineCalculationDetail)
                    .ThenInclude(fd => fd.valueSmldv)
            .Include(p => p.paymentFrequency)
            .Include(p => p.TypePayment)
            .Where(p => !p.is_deleted)
            .ToListAsync();
    }

    public override async Task<IEnumerable<PaymentAgreement>> GetDeletes()
    {
        return await _dbSet
            .Include(p => p.userInfraction)
                .ThenInclude(ui => ui.User)
                    .ThenInclude(u => u.Person)
            .Include(p => p.userInfraction.User.documentType)
            .Include(p => p.userInfraction.typeInfraction)
                .ThenInclude(ti => ti.fineCalculationDetail)
                    .ThenInclude(fd => fd.valueSmldv)
            .Include(p => p.paymentFrequency)
            .Include(p => p.TypePayment)
            .Where(p => p.is_deleted == true)
            .ToListAsync();
    }

    public override async Task<PaymentAgreement?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.userInfraction)
                .ThenInclude(ui => ui.User)
                    .ThenInclude(u => u.Person)
            .Include(p => p.userInfraction.User.documentType)
            .Include(p => p.userInfraction.typeInfraction)
                .ThenInclude(ti => ti.fineCalculationDetail)
                    .ThenInclude(fd => fd.valueSmldv)
            .Include(p => p.paymentFrequency)
            .Include(p => p.TypePayment)
            .FirstOrDefaultAsync(p => p.id == id); 
    }

    public async Task<IEnumerable<PaymentAgreementInitDto>> GetInitDataAsync(int userInfractionId)
    {
        var infractions = await _context.userInfraction
            .Where(ui => ui.User.id == userInfractionId) // 👈 todas las multas de ese usuario
            .Include(ui => ui.User)
                .ThenInclude(u => u.Person)
            .Include(ui => ui.User)
                .ThenInclude(u => u.documentType)
            .Include(ui => ui.typeInfraction)
                .ThenInclude(ti => ti.fineCalculationDetail)
                    .ThenInclude(fd => fd.valueSmldv)
            .ToListAsync();

        return infractions.Select(infraction => new PaymentAgreementInitDto
        {
            PersonName = $"{infraction.User.Person?.firstName ?? ""} {infraction.User.Person?.lastName ?? ""}".Trim(),
            DocumentNumber = infraction.User?.documentNumber ?? string.Empty,
            DocumentType = infraction.User?.documentType?.name ?? string.Empty,
            InfractionId = infraction.id,  // 👈 aquí guardamos el ID de la multa
            Infringement = infraction.observations ?? string.Empty,
            TypeFine = infraction.typeInfraction?.description ?? string.Empty,
            ValorSMDLV = (decimal)(infraction.typeInfraction?.fineCalculationDetail?
                            .FirstOrDefault()?.valueSmldv?.value_smldv ?? 0),

            UserId = infraction.UserId
        });
    }

}

