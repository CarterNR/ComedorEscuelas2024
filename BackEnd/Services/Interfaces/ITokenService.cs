using BackEnd.DTO;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.Interfaces
{
    public interface ITokenService
    {
        TokenModel CreateToken(IdentityUser user, List<string> roles);
    }
}
