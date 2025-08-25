using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.Extensions.Logging;

namespace Business.Services.Entities
{
    public class TypePaymentServices : BusinessBasic<TypePaymentDto,TypePaymentSelectDto,TypePayment>,ITypePaymentServices
    {
        private readonly ILogger<TypePaymentServices> _logger;

        protected readonly IData<TypePayment> Data;

        public TypePaymentServices(IData<TypePayment> data, IMapper mapper, ILogger<TypePaymentServices> logger) : base(data, mapper)
        {
            Data = data;
            _logger = logger;
        }
    }
}
