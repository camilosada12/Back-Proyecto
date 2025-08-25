using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.Entities;
using Microsoft.Extensions.Logging;
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

       
    }
}
