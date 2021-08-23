using System;
using System.Threading;
using System.Threading.Tasks;
using API.BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace API.BLL.Request
{
    public class CourseAssignInsertViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }

    public class CourseAssignInsertViewModelValidator : AbstractValidator<CourseAssignInsertViewModel>
    {
        private readonly IServiceProvider services;

        public CourseAssignInsertViewModelValidator(IServiceProvider services)
        {
            this.services = services;
            RuleFor(x => x.StudentId).NotNull().NotEmpty().MustAsync(StudentIdExists).WithMessage("{PropertyName} not exists");
            RuleFor(x => x.CourseId).NotNull().NotEmpty().MustAsync(CourseIdExists).WithMessage("{PropertyName} not exists");
        }
        private async Task<bool> StudentIdExists(int studentId, CancellationToken arg2)
        {
            var requiredService = services.GetRequiredService<IStudentService>();
            return !await requiredService.IsIdExists(studentId);
        }

        private async Task<bool> CourseIdExists(int courseId, CancellationToken arg2)
        {
            var requiredService = services.GetRequiredService<ICourseService>();
            return await requiredService.IsCourseIdExists(courseId);
        }
    }
}