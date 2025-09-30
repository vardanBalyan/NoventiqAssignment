using CSharpFunctionalExtensions;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.Framework.Framework;

namespace NoventiqAssignment.APP.Services
{
    public interface IRoleService
    {
        ValueTask<Result<string, DefaultFailure>> CreateRoleAsync(RoleDto model);
        ValueTask<Result<GetRoleDto, DefaultFailure>> GetRolesByNameAsync(string name);
        ValueTask<Result<List<GetRoleDto>, DefaultFailure>> GetAllRolesAsync();
        ValueTask<Result<bool, DefaultFailure>> UpdateRoleAsync(UpdateRoleDto model);
        ValueTask<Result<bool, DefaultFailure>> DeleteRoleAsync(RoleDto model);
    }
}
