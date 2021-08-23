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
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICourseStudentService, CourseStudentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITestService, TestService>();
            #endregion

            services.AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}