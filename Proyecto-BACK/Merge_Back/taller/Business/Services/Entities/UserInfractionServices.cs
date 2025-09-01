using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.IDataImplement.Entities;   // <- IUserInfractionRepository
using Data.Interfaces.IDataImplement.Security;   // <- IUserRepository
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

public class UserInfractionServices
    : BusinessBasic<UserInfractionDto, UserInfractionSelectDto, UserInfraction>, IUserInfractionServices
{
    private readonly ILogger<UserInfractionServices> _logger;
    private readonly IUserInfractionRepository _repo;       // CRUD principal
    private readonly IUserRepository _users;                 // FK -> User
    private readonly ITypeInfractionRepository _types;      // FK -> TypeInfraction
    private readonly IUserNotificationRepository _notifs;   // FK -> UserNotification

    public UserInfractionServices(
        IUserInfractionRepository repo,
        IUserRepository users,
        ITypeInfractionRepository types,
        IUserNotificationRepository notifs,
        IMapper mapper,
        ILogger<UserInfractionServices> logger,
        Entity.Infrastructure.Contexts.ApplicationDbContext db
    ) : base(repo, mapper, db)
    {
        _repo = repo;
        _users = users;
        _types = types;
        _notifs = notifs;
        _logger = logger;
    }

    // -------- Helpers FK --------
    private async Task EnsureFkAsync(UserInfractionDto dto)
    {
        // FK: userId
        if (await _users.GetByIdAsync(dto.userId) is null)
            throw new BusinessException($"El usuario con ID {dto.userId} no existe.");

        // FK: typeInfractionId
        if (await _types.GetByIdAsync(dto.typeInfractionId) is null)
            throw new BusinessException($"El tipo de infracción con ID {dto.typeInfractionId} no existe.");

        // FK: UserNotificationId
        if (await _notifs.GetByIdAsync(dto.UserNotificationId) is null)
            throw new BusinessException($"La notificación de usuario con ID {dto.UserNotificationId} no existe.");
    }

    public override async Task<UserInfractionSelectDto?> GetByIdAsync(int id)
    {
        BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new BusinessException($"La infracción de usuario con ID {id} no existe.");

        return _mapper.Map<UserInfractionSelectDto>(entity);
    }

    public override async Task<UserInfractionDto> CreateAsync(UserInfractionDto dto)
    {
        BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");
        await EnsureFkAsync(dto);

        return await base.CreateAsync(dto);
    }

    public override async Task<bool> UpdateAsync(UserInfractionDto dto)
    {
        BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");
        BusinessValidationHelper.ThrowIfZeroOrLess(dto.id, "El ID debe ser mayor que cero.");

        if (!await ExistsAsync(dto.id))
            throw new BusinessException($"La infracción de usuario con ID {dto.id} no existe.");

        await EnsureFkAsync(dto);
        return await base.UpdateAsync(dto);
    }

    public override async Task<bool> DeleteAsync(int id)
    {
        BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

        if (!await ExistsAsync(id))
            throw new BusinessException($"No se puede eliminar. La infracción de usuario con ID {id} no existe.");

        return await base.DeleteAsync(id);
    }

    public override async Task<bool> RestoreLogical(int id)
    {
        BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

        if (!await ExistsAsync(id))
            throw new BusinessException($"No se puede restaurar. La infracción de usuario con ID {id} no existe.");

        return await base.RestoreLogical(id);
    }

    public override async Task<IEnumerable<UserInfractionSelectDto>> GetAllAsync(GetAllType getAllType)
    {
        var strategy = GetStrategyFactory.GetStrategyGet(_repo, getAllType);
        var entities = await strategy.GetAll(_repo);
        return _mapper.Map<IEnumerable<UserInfractionSelectDto>>(entities);
    }
}

