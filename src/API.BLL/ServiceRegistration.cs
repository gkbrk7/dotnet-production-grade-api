using System.Reflection;
using API.BLL.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.BLL
{
    public static class ServiceRegistration
    {
        public static void AddBllDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region ServiceDependencies
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
            #endregion

            services.AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}