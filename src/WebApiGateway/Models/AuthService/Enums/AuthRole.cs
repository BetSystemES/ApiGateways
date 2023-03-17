using System.ComponentModel;

namespace WebApiGateway.Models.AuthService.Enums
{
    public enum AuthRole
    {
        [Description("admin")]
        Admin,

        [Description("user")]
        User,
    }
}