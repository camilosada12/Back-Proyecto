using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Default.EntitiesDto;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using Utilities.Exceptions;

namespace Business.Services.Entities
{
    public class InspectoraReportService
        : BusinessBasic<InspectoraReportDto, InspectoraReportSelectDto, InspectoraReport>, IInspectoraReportService
    {
        private readonly ILogger<InspectoraReportService> _logger;
        protected readonly IData<InspectoraReport> Data;

        public InspectoraReportService(
            IData<InspectoraReport> data,
            IMapper mapper,
            ILogger<InspectoraReportService> logger
        ) : base(data, mapper)
        {
            Data = data;
            _logger = logger;
        }

        public async Task<InspectoraPdfDto> GetByIdAsyncPdf(int id)
        {
            try
            {
                var entity = await Data.GetByIdAsync(id);
                if (entity == null)
                {
                    throw new NotFoundException($"InspectoraReport con ID {id} no encontrado.");
                }
                return _mapper.Map<InspectoraPdfDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener InspectoraReport con ID {id}.");
                throw new BusinessException($"Error al obtener InspectoraReport con ID {id}.", ex);
            }
        }


    }
}
