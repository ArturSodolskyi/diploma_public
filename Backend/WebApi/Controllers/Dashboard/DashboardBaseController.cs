using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/dashboard/[controller]/[action]")]
    public abstract class DashboardBaseController : BaseController
    {
    }
}
