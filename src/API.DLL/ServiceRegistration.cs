using API.DLL.Contexts;
using API.DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.DLL
{
    public static class ServiceRegistration
    {
        public static void AddDllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #region RepositoryDependencies
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}