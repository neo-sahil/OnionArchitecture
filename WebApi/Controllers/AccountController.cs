using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.IServices;

namespace WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        ////[HttpPost("register")]
        //[HttpPost]
        //public async Task<TResponse> RegisterUser(UserDto obj)
        //{
        //    return await _accountService.regiterUser(obj);
        //}

        //[HttpPost("login")]
        [HttpPost]
        public async Task<TResponse> LogIn(LogInDTO obj)
        {
            return await _accountService.logIn(obj);
        }
    }
}
