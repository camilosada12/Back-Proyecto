using Business.Mensajeria.Email.implements;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.@interface
{
    public interface IVerificationService
    {
        // Registro inicial → manda código y pone en estado Pending
        Task SendVerificationAsync(string nombre, string email);

        // Validar un código → si es correcto activa la cuenta
        bool ValidateCode(string email, string code);

        // Envío genérico con un builder (se mantiene)
        Task SendEmailAsync(string email, VerificacionEmailBuilder builder);

        // Nuevo: verificar reactivación de cuenta bloqueada
        Task<bool> ReactivateAccountAsync(string email, string code);

        // Nuevo: envío de código de reactivación (cuando el usuario bloqueado quiere recuperar acceso)
        Task SendReactivationCodeAsync(string email);

        // Nuevo: para el worker mensual, enviar código de revalidación a un usuario activo
        Task SendMonthlyReverificationAsync(string email, string nombre);
    }
}
