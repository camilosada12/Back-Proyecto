using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Models.Implements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Entities
{
    public class FineCalculationDetailService : BusinessBasic<FineCalculationDetailDto, FineCalculationDetailSelectDto, FineCalculationDetail>,
            IFineCalculationDetailService
    {
        private readonly IFineCalculationDetailRepository _repository;
        private readonly IValueSmldvRepository _valueSmldvRepository;
        private readonly IMapper _mapper;

        public FineCalculationDetailService(
            IFineCalculationDetailRepository repository,
            IMapper mapper,
            IValueSmldvRepository valueSmldvRepository) // 👈 se inyecta también el IMapper
            : base(repository, mapper) // 👈 ahora pasas los dos parámetros requeridos
        {
            _repository = repository;
            _valueSmldvRepository = valueSmldvRepository;
            _mapper = mapper;
        }

        // 🔎 Si quieres lógica extra además de lo que hace BusinessBasic
        // Si estás redefiniendo el comportamiento:
        public override async Task<FineCalculationDetailSelectDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<FineCalculationDetailSelectDto>(entity);
        }


        public async Task<FineCalculationDetail> CreateAsync(FineCalculationDetailDto dto)
        {
            Validate(dto);

            if (dto.percentaje > 100)
                throw new InvalidOperationException("El porcentaje no puede ser mayor a 100.");

            // 🔎 Buscar el valor real en la BD
            var valueSmldv = await _valueSmldvRepository.GetByIdAsync(dto.valueSmldvId);
            if (valueSmldv == null)
                throw new InvalidOperationException("No existe el valor de SMLDV seleccionado.");

            // Calcular total si no viene
            if (dto.totalCalculation <= 0)
                dto.totalCalculation = dto.percentaje * (decimal)valueSmldv.value_smldv / 100;

            // Mapear DTO → Entidad
            var entity = _mapper.Map<FineCalculationDetail>(dto);

            return await _repository.CreateAsync(entity);
        }

        public async Task<FineCalculationDetail> UpdateAsync(FineCalculationDetailDto dto)
        {
            if (dto.id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            Validate(dto);

            var entity = _mapper.Map<FineCalculationDetail>(dto);

            await _repository.UpdateAsync(entity);

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El Id debe ser mayor que cero.");

            return await _repository.DeleteAsync(id);
        }

        // 🔎 Validaciones personalizadas
        protected void Validate(FineCalculationDetailDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "El cálculo no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(dto.forumula))
                throw new ArgumentException("La fórmula es obligatoria.");

            if (dto.percentaje < 0 || dto.percentaje > 100)
                throw new ArgumentException("El porcentaje debe estar entre 0 y 100.");

            if (dto.valueSmldvId <= 0)
                throw new ArgumentException("Debe asociarse un valor de SMLDV válido.");

            if (dto.typeInfractionId <= 0)
                throw new ArgumentException("Debe asociarse un tipo de infracción válido.");
        }
    }
}


    

