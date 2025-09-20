﻿using Business.Interfaces.BusinessBasic;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Select.Entities;
using Entity.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.IBusinessImplements.Entities
{
    public interface IPaymentAgreementServices : IBusiness<PaymentAgreementDto,PaymentAgreementSelectDto>
    {
        Task<int> ApplyLateFeesAsync(DateTime nowUtc, CancellationToken ct = default);
        Task<IEnumerable<PaymentAgreementInitDto>> GetInitDataAsync(int userInfractionId);

        new Task<PaymentAgreementSelectDto?> CreateAsync(PaymentAgreementDto dto);

        Task<PaymentAgreementSelectDto> GetByIdAsyncPdf(int id);

    }
}
