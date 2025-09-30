using CSharpFunctionalExtensions;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.Framework.Framework;

namespace NoventiqAssignment.APP.Services
{
    public interface IUserService
    {
        ValueTask<Result<TokenResponseDto, DefaultFailure>> RegisterAsync(RegisterDto model);
        ValueTask<Result<TokenResponseDto, DefaultFailure>> LoginUserAsync(LoginDto model);
        ValueTask<Result<TokenResponseDto, DefaultFailure>> RefreshTokenAsync(RefreshTokenDto model);
        ValueTask<Result<GetUserDto, DefaultFailure>> GetUserByUserNameAsync(string username);
        ValueTask<Result<bool, DefaultFailure>> UpdateUserAsync(UpdateUserDto model);
        ValueTask<Result<bool, DefaultFailure>> DeleteUserAsync(string username);
    }
}
