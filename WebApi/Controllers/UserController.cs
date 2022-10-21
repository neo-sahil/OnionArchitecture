using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.IServices;

namespace WebApi.Controllers
{
    //[Authorize(Roles = "Noob")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<TResponse> GetUsers()
        {
            return await _userService.GetUsers();
        }

        //[HttpGet]
        //public async Task<TResponse> GetUsers()
        //{
        //    return await _usersService.GetUsers();
        //}

        //[HttpGet("{id}")]
        [HttpGet]
        public async Task<TResponse> GetUsersById(int id)
        {
            return await _userService.GetUserById(id);
        }

        //[HttpGet("{name}")]
        [HttpGet]
        public async Task<TResponse> GetMembersByName(string name)
        {
            return await _userService.GetUserByName(name);
        }
        [HttpGet]
        public async Task<TResponse> GetMembers()
        {
            return await _userService.GetMembersAsync();
        }
    }
}
