using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.DAL.Entities;
using NoventiqAssignment.Framework.Framework;

namespace NoventiqAssignment.APP.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async ValueTask<Result<string, DefaultFailure>> CreateRoleAsync(RoleDto model)
        {
            var role = new IdentityRole
            { 
                Name = model.RoleName,
                NormalizedName = model.RoleName.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded) return Result.Failure<string, DefaultFailure>(new DefaultFailure(result.Errors.Select(s => s.Description)));

            return "Role created sucessfully.";
        }

        public async ValueTask<Result<bool, DefaultFailure>> DeleteRoleAsync(RoleDto model)
        {
            if (!await _roleManager.RoleExistsAsync(model.RoleName))
                return Result.Failure<bool, DefaultFailure>(new DefaultFailure([$"No role exists with name {model.RoleName}"]));

            var roleUsers = await _userManager.GetUsersInRoleAsync(model.RoleName);

            if (roleUsers.Any())
                return Result.Failure<bool, DefaultFailure>(new DefaultFailure([$"Deletion failed: Role {model.RoleName} is assigned to one or more users. Unassign the role before deleting."]));

            var role = await _roleManager.FindByNameAsync(model.RoleName);

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded) return Result.Failure<bool, DefaultFailure>(new DefaultFailure(result.Errors.Select(x => x.Description)));

            return true;
        }

        public async ValueTask<Result<List<GetRoleDto>, DefaultFailure>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var result = roles?.Select(x => new GetRoleDto { Id = x.Id, Name = x.Name})?.ToList();

            return result;
        }

        public async ValueTask<Result<GetRoleDto, DefaultFailure>> GetRolesByNameAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);

            if(role == null) return Result.Failure<GetRoleDto, DefaultFailure>(new DefaultFailure([$"No role exists with name {name}"]));

            return new GetRoleDto { Id = role.Id, Name = role.Name };
        }

        public async ValueTask<Result<bool, DefaultFailure>> UpdateRoleAsync(UpdateRoleDto model)
        {
            if(!await _roleManager.RoleExistsAsync(model.OldName))
                return Result.Failure<bool, DefaultFailure>(new DefaultFailure([$"No role exists with name {model.OldName}"]));

            var role = await _roleManager.FindByNameAsync(model.OldName);

            role.Name = model.NewName;
            role.NormalizedName = model.NewName.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            if(!result.Succeeded) return Result.Failure<bool, DefaultFailure>(new DefaultFailure(result.Errors.Select(x => x.Description)));

            return true;

        }
    }
}
