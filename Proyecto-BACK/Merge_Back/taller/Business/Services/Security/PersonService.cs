using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.Security
{
    public class PersonService : BusinessBasic<PersonDto, PersonSelectDto, Person>, IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        //protected override IData<Person> Data => _unitOfWork.Persons;
        protected readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository data, IMapper mapper, ILogger<PersonService> logger) : base(data, mapper)
        {
            _personRepository = data;
            _logger = logger;
        }

        public override async Task<IEnumerable<PersonSelectDto>> GetAllAsync(GetAllType getAllType)
        {
            try
            {


                var strategy = GetStrategyFactory.GetStrategyGet(_personRepository, getAllType);
                var entities = await strategy.GetAll(_personRepository);
                return _mapper.Map<IEnumerable<PersonSelectDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
        }

        public override async Task<PersonSelectDto?> GetByIdAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                var entity = await _personRepository.GetByIdAsync(id);
                return _mapper.Map<PersonSelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }

        }


        //protected override void ValidateDto(PersonDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ValidationException("El objeto Person no puede ser nulo");
        //    }

        //    if (string.IsNullOrWhiteSpace(dto.first_name))
        //    {
        //        _logger.LogWarning("Se intentó crear/actualizar una Person con Name vacío");
        //        throw new ValidationException("name", "El Name de la Person es obligatorio");
        //    }
        //}

        //protected override async Task ValidateIdAsync(int id)
        //{
        //    var entity = await Data.GetByIdAsync(id);
        //    if (entity == null)
        //    {
        //        _logger.LogWarning($"Se intentó operar un ID inválido: {id}");
        //        throw new EntityNotFoundException($"No se encontró un Person con el ID {id}");
        //    }
        //}
    }
}
