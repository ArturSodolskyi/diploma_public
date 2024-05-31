using System.ComponentModel;

namespace Shared.Models
{
    public enum RoleEnum
    {
        [Description("User")]
        User = 1,
        [Description("Administrator")]
        Administrator,
    }
}
