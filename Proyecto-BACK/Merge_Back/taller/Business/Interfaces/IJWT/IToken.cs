using Entity.DTOs.Default.LoginDto;
using Google.Apis.Auth;
namespace Business.Interfaces.IJWT
{
    public interface IToken
    {
        Task<string> GenerateToken(LoginDto dto);
        bool validarToken(string token);
        Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string tokenId);
    }
}
