using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.DAL.Entities;
using NoventiqAssignment.Framework.Framework;
using System.IdentityModel.Tokens.Jwt;

namespace NoventiqAssignment.APP.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async ValueTask<Result<bool, DefaultFailure>> DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return Result.Failure<bool, DefaultFailure>(new(["Invalid username"]));

            await _userManager.DeleteAsync(user);

            return true;
        }

        public async ValueTask<Result<GetUserDto, DefaultFailure>> GetUserByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return Result.Failure<GetUserDto, DefaultFailure>(new([$"No user exists with username {username}"]));  

            var userRole = await _userManager.GetRolesAsync(user);

            return new GetUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Role = userRole.FirstOrDefault() ?? ""
            };
        }

        public async ValueTask<Result<TokenResponseDto, DefaultFailure>> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) Result.Failure<TokenResponseDto, DefaultFailure>(new([]));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Result.Failure<TokenResponseDto, DefaultFailure>(new([]));

            var token = await _tokenService.CreateTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto { Token = token.Value, RefreshToken = refreshToken };
        }

        public async ValueTask<Result<TokenResponseDto, DefaultFailure>> RefreshTokenAsync(RefreshTokenDto model)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(model.Token);
            if (principal == null)
            {
                return Result.Failure<TokenResponseDto, DefaultFailure>(new(["Invalid access token or refresh token"]));
            }

            var username = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value?.ToString() ?? "";
            var user = await _userManager.FindByNameAsync(username);

            if (user == null ||
                user.RefreshToken != model.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Result.Failure<TokenResponseDto, DefaultFailure>(new(["Invalid refresh token"]));
            }

            var newJwtToken = await _tokenService.CreateTokenAsync(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                Token = newJwtToken.Value,
                RefreshToken = newRefreshToken
            };
        }

        public async ValueTask<Result<TokenResponseDto, DefaultFailure>> RegisterAsync(RegisterDto model)
        {
            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            if (!await _roleManager.RoleExistsAsync(model.Role))
                return Result.Failure<TokenResponseDto, DefaultFailure>(new ([$"{model.Role} role does not exists"]));

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return Result.Failure<TokenResponseDto, DefaultFailure>(new (result.Errors.Select(s => s.Description)));

            await _userManager.AddToRoleAsync(user, model.Role);

            var token = await _tokenService.CreateTokenAsync(user);

            return new TokenResponseDto { Token = token.Value, RefreshToken = user.RefreshToken };
        }

        public async ValueTask<Result<bool, DefaultFailure>> UpdateUserAsync(UpdateUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) return Result.Failure<bool, DefaultFailure>(new(["Invalid username"]));

            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return Result.Failure<bool, DefaultFailure>(new(result.Errors.Select(s => s.Description)));

            return true;
        }
    }
}
