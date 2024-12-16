using Microsoft.AspNetCore.Identity;

namespace Meetings_App.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user);
    }
}
