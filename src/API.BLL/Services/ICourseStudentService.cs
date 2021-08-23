using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Utilities.Exceptions;
using API.BLL.Utilities.Models;
using API.DLL.Models;
using API.DLL.Repositories;
using API.DLL.ResponseViewModels;

namespace API.BLL.Services
{
    public interface ICourseStudentService
    {
        Task<ApiSuccessResponse> InsertAsync(CourseAssignInsertViewModel request);
        Task<StudentCourseViewModel> CourseListAsync(int studentId);
    }

    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<StudentCourseViewModel> CourseListAsync(int studentId)
        {
            return await unitOfWork.StudentRepository.GetSpecificStudentCourseListAsync(studentId);
        }

        public async Task<ApiSuccessResponse> InsertAsync(CourseAssignInsertViewModel request)
        {
            var isStudentAlreadyEnroll = await unitOfWork.CourseStudentRepository.FindSingleAsync(x => x.StudentId == request.StudentId && x.CourseId == request.CourseId);

            if (isStudentAlreadyEnroll != null)
            {
                throw new ApplicationValidationException($"Student already enrolled this course.");
            }

            var courseStudent = new CourseStudent
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };

            await unitOfWork.CourseStudentRepository.CreateAsync(courseStudent);
            if (await unitOfWork.SaveChangesAsync())
            {
                return new ApiSuccessResponse
                {
                    Message = "Student enrolled successfully."
                };
            }

            throw new ApplicationValidationException($"Something went wrong for enrollment operation.");
        }
    }
}