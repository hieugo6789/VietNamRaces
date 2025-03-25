using Microsoft.AspNetCore.Identity;

namespace VietNamRaces.API.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
