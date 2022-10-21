using Application.Intrefaces;
using Application.Intrefaces.Repository.Base;
using Application.Intrefaces.Repository.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository.Base;
using Persistence.Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ind"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            #region Repo
            services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepo, UserRepo>();
            #endregion
        }
    }
}
