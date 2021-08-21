using System;
using System.Threading;
using System.Threading.Tasks;
using API.BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace API.BLL.Request
{
    public class CourseInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credit { get; set; }
    }

    public class CourseInsertRequestViewModelValidator : AbstractValidator<CourseInsertRequestViewModel>
    {
        private readonly IServiceProvider services;

        public CourseInsertRequestViewModelValidator(IServiceProvider services)
        {
            this.services = services;
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(25).MustAsync(NameExists).WithMessage("{PropertyName} already exists");
            RuleFor(x => x.Code).NotNull().NotEmpty().MaximumLength(3).MaximumLength(10).MustAsync(CodeExists).WithMessage("{PropertyName} already exists");
            RuleFor(x => x.Credit).NotNull().NotEmpty().WithMessage("{PropertyName} is required");

        }
        private async Task<bool> CodeExists(string code, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(code))
                return true;

            var requiredService = services.GetRequiredService<ICourseService>();
            return await requiredService.IsCodeExists(code);
        }

        private async Task<bool> NameExists(string name, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            var requiredService = services.GetRequiredService<ICourseService>();
            return await requiredService.IsNameExists(name);
        }
    }
}