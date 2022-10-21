using Application.EntityDTO;
using Application.Intrefaces.Repository.Repos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repository.Base;
using System.Linq.Expressions;

namespace Persistence.Repository.Repos
{
    public class UserRepo : GenericRepo<AppUser>, IUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepo(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> FindUserAsync(Expression<Func<AppUser, bool>> expression)
        {
            return await _context.Users.Include(p => p.Photos).Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.Include(p => p.Photos).ToListAsync();
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MemberUserDto>> GetAllMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberUserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<MemberUserDto> GetMembersAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).ProjectTo<MemberUserDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
    }
}
