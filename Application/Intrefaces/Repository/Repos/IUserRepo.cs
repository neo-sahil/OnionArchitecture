using Application.EntityDTO;
using Application.Intrefaces.Repository.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Intrefaces.Repository.Repos
{
    public interface IUserRepo :IGenericRepo<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<IEnumerable<AppUser>> FindUserAsync(Expression<Func<AppUser, bool>> expression);
        Task<IEnumerable<MemberUserDto>> GetAllMembersAsync();
        Task<MemberUserDto> GetMembersAsync(string username);
    }
}
