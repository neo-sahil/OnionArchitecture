using Application.Intrefaces.Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Intrefaces.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        Task<int> Complete();
    }
}
