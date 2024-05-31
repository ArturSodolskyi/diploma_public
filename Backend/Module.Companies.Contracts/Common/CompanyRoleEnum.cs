using System.ComponentModel;

namespace Module.Companies.Contracts.Common
{
    public enum CompanyRoleEnum
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("Watcher")]
        Watcher
    }
}
