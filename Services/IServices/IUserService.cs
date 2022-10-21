using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUserService
    {
        Task<TResponse> GetUsers();
        Task<TResponse> GetUserById(int id);
        Task<TResponse> GetUserByName(string name);
        Task<TResponse> GetMembersAsync();
    }
}
