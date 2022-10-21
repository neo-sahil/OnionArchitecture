using Application.Intrefaces.Repository.Base;
using Application.Intrefaces.Repository.Repos;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IUserRepo userRepo)
        {
            _context = context;
            UserRepo = userRepo;
        }
        public IUserRepo UserRepo { get; private set; }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
