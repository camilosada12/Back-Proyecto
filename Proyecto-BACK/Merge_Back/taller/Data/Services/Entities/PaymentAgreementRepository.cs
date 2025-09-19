using Data.Interfaces.IDataImplement.Entities;
using Data.Repositoy;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.parameters;
using Entity.Infrastructure.Contexts;
using Entity.Init;
using Microsoft.EntityFrameworkCore;
using Utilities.Exceptions;

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

        return infractions.Select(infraction =>
        {
            // 👇 Tomamos el detalle de cálculo más reciente
            var detail = infraction.typeInfraction?.fineCalculationDetail?
                .OrderByDescending(fd => fd.valueSmldv.Current_Year)
                .FirstOrDefault();

            return new PaymentAgreementInitDto
            {
                PersonName = $"{infraction.User.Person?.firstName ?? ""} {infraction.User.Person?.lastName ?? ""}".Trim(),
                DocumentNumber = infraction.User?.documentNumber ?? string.Empty,
                DocumentType = infraction.User?.documentType?.name ?? string.Empty,
                InfractionId = infraction.id,
                Infringement = infraction.observations ?? string.Empty,
                TypeFine = infraction.typeInfraction?.description ?? string.Empty,

                // Valor unitario de SMDLV (por si el front lo necesita mostrar)
                ValorSMDLV = (decimal)(detail?.valueSmldv?.value_smldv ?? 0),

                // ✅ Ahora el monto base ya viene precalculado en FineCalculationDetail
                BaseAmount = detail != null
                ? (detail.totalCalculation > 0
                    ? detail.totalCalculation
                    : (detail.typeInfraction?.numer_smldv ?? 0) * (decimal)(detail.valueSmldv?.value_smldv ?? 0))
                : 0,


                UserId = infraction.UserId
            };
        });
    }

    public async Task<UserInfraction?> GetUserInfractionWithDetailsAsync(int userInfractionId)
    {
        return await _context.userInfraction
            .Include(ui => ui.User)
                .ThenInclude(u => u.Person)
            .Include(ui => ui.typeInfraction)
                .ThenInclude(ti => ti.fineCalculationDetail)
                    .ThenInclude(fd => fd.valueSmldv)
            .FirstOrDefaultAsync(ui => ui.id == userInfractionId);
    }

    public async Task<PaymentFrequency?> GetPaymentFrequencyAsync(int id)
    {
        return await _context.paymentFrequency.FindAsync(id);
    }

    public async Task<TypePayment?> GetTypePaymentAsync(int id)
    {
        return await _context.typePayment.FindAsync(id);
    }


}

