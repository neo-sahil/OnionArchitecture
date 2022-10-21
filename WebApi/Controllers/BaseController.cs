using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.MIddleWare;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected CurrentUser? CurrentUser => (CurrentUser)HttpContext?.Items["CurrentUser"];

        #region  Methods

        protected string IpAddress()
        {
            return Request.Headers.ContainsKey("X-Forwarded-For")
                ? (string)Request.Headers["X-Forwarded-For"]
                : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        #endregion
    }
}
