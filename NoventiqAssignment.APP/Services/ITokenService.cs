using CSharpFunctionalExtensions;
using NoventiqAssignment.DAL.Entities;
using System.Security.Claims;

namespace NoventiqAssignment.APP.Services
{
    public interface ITokenService
    {
        ValueTask<Result<string>> CreateTokenAsync(AppUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
}
