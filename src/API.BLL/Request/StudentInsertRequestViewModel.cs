using System;
using System.Threading;
using System.Threading.Tasks;
using API.BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace API.BLL.Request
{
    public class StudentInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }
    public class StudentInsertRequestViewModelValidator : AbstractValidator<StudentInsertRequestViewModel>
    {
        private readonly IServiceProvider services;

        public StudentInsertRequestViewModelValidator(IServiceProvider services)
        {
            this.services = services;
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(50).MustAsync(NameExists).WithMessage("{PropertyName} already exists");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MustAsync(EmailExists).WithMessage("{PropertyName} already exists");
            RuleFor(x => x.DepartmentId).GreaterThan(0).MustAsync(DepartmentExists).WithMessage("{PropertyName} is invalid");
        }

        private async Task<bool> DepartmentExists(int departmentId, CancellationToken arg2)
        {
            var requiredService = services.GetRequiredService<IDepartmentService>();
            return await requiredService.IsIdExists(departmentId);
        }

        private async Task<bool> EmailExists(string email, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(email))
                return true;

            var requiredService = services.GetRequiredService<IStudentService>();
            return await requiredService.EmailExists(email);
        }

        private async Task<bool> NameExists(string name, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            var requiredService = services.GetRequiredService<IDepartmentService>();
            return await requiredService.IsNameExists(name);
        }
    }

}