namespace Lifecare_Backend.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public bool IsAllowed { get; set; }
        public bool IsLocked { get; set; }
    }
}
