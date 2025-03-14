using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpticsWebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {

    }
}
