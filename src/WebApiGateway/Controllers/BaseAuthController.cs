using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiGateway.Filters;

namespace WebApiGateway.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(AuthFilter))]
    public class BaseAuthController : Controller
    {
    }
}
