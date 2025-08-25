using Entity.Domain.Enums;

namespace Business.Interfaces.BusinessBasic
{
    public interface IBusiness<TDto, TDtoGet>
    {
        Task<TDtoGet?> GetByIdAsync(int id);
        Task<IEnumerable<TDtoGet>> GetAllAsync();
        Task<IEnumerable<TDtoGet>> GetAllAsync(GetAllType g);
        Task<TDto> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(int id, DeleteType deleteType);
        Task<bool> RestoreLogical(int id);

        //Metodos de validacion

        Task ValidateForCreateAsync(TDto dto) => Task.CompletedTask;
        Task ValidateForUpdateAsync(TDto dto) => Task.CompletedTask;
        Task ValidateIdAsync(int id) => Task.CompletedTask;

    }
}
