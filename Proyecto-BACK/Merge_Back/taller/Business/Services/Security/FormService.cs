using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Data.Interfaces.DataBasic;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Initialize;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;
using Utilities.Exceptions;

namespace Business.Services.Security
{
    public class FormService : BusinessBasic<FormDto, FormSelectDto, Form>, IFormService
    {
        private readonly ILogger<FormService> _logger;
        //protected override IData<Form> Data => _unitOfWork.Forms;
        protected readonly IData<Form> Data;
        public FormService(IData<Form> data, IMapper mapper, ILogger<FormService> logger) : base(data, mapper)
        {
            Data = data;
            _logger = logger;
        }

        public override async Task<bool> UpdateAsync(FormDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ValidationException("El DTO no puede ser nulo.");

                // Traer el registro actual de BD
                var existing = await Data.GetByIdAsync(dto.id);
                if (existing == null)
                    throw new EntityNotFoundException($"No se encontró un Form con ID {dto.id}");

                // Chequear cambios reales (ignorar espacios)
                var newName = dto.name?.Trim();
                var newDesc = dto.description?.Trim();
                var oldName = existing.name?.Trim();
                var oldDesc = existing.description?.Trim();

                if (newName == oldName && newDesc == oldDesc)
                {
                    throw new BusinessException("Debe realizar al menos un cambio para actualizar el formulario.");
                }

                // Mapear sobre la entidad existente en vez de crear una nueva
                existing.name = newName;
                existing.description = newDesc;
                existing.InitializeLogicalState();

                // Guardar cambios
                return await Data.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is ValidationException)
                    ExceptionDispatchInfo.Capture(ex).Throw();

                throw new BusinessException("Error al actualizar el formulario.", ex);
            }
        }




        //protected override void ValidateDto(FormDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ValidationException("El objeto Form no puede ser nulo");
        //    }

        //    if (string.IsNullOrWhiteSpace(dto.name))
        //    {
        //        _logger.LogWarning("Se intentó crear/actualizar una Form con Name vacío");
        //        throw new ValidationException("name", "El Name de la Form es obligatorio");
        //    }
        //}

        //protected override async Task ValidateIdAsync(int id)
        //{
        //    var entity = await Data.GetByIdAsync(id);
        //    if (entity == null)
        //    {
        //        _logger.LogWarning($"Se intentó operar un ID inválido: {id}");
        //        throw new EntityNotFoundException($"No se encontró un Form con el ID {id}");
        //    }
        //}
    }
}
