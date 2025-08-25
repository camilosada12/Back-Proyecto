using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.Entities
{
    public class DocumentInfractionServices : BusinessBasic<DocumentInfractionDto,DocumentInfractionSelectDto,DocumentInfraction>, IDocumentInfractionServices
    {
        private readonly ILogger<DocumentInfractionServices> _logger;

        protected readonly IDocumentInfractionRepository _DocumentInfractionRepository;

        public DocumentInfractionServices(IDocumentInfractionRepository data, IMapper mapper,ILogger<DocumentInfractionServices> logger) : base(data, mapper)
        {
            _DocumentInfractionRepository = data;
            _logger = logger;
        }

        public override async Task<IEnumerable<DocumentInfractionSelectDto>> GetAllAsync(GetAllType getAllType)
        {
            try
            {

                var strategy = GetStrategyFactory.GetStrategyGet(_DocumentInfractionRepository, getAllType);
                var entities = await strategy.GetAll(_DocumentInfractionRepository);
                return _mapper.Map<IEnumerable<DocumentInfractionSelectDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
        }

        public override async Task<DocumentInfractionSelectDto?> GetByIdAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                var entity = await _DocumentInfractionRepository.GetByIdAsync(id);
                return _mapper.Map<DocumentInfractionSelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }

        }
    }
}
