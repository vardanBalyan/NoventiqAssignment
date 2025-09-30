namespace NoventiqAssignment.APP.Dtos
{
    public class RoleDto
    {
        public string RoleName { get; set; }
    }

    public class GetRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateRoleDto
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
