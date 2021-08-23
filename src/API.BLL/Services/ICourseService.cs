using System.Collections.Generic;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Utilities.Exceptions;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetAsync(string code);
        Task<Course> InsertAsync(CourseInsertRequestViewModel request);
        Task<Course> UpdateAsync(string code, Course course);
        Task<Course> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
        Task<bool> IsIdExists(int courseId);
        Task<bool> IsCourseIdExists(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Course> DeleteAsync(string code)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
                throw new ApplicationValidationException("Course Not Found");

            unitOfWork.CourseRepository.DeleteAsync(course);

            if (await unitOfWork.CourseRepository.SaveCompletedAsync())
                return course;

            throw new ApplicationValidationException("An Error Occured Deleting Data");
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await unitOfWork.CourseRepository.GetList();
        }

        public async Task<Course> GetAsync(string code)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
                throw new ApplicationValidationException("Course Not Found");

            return course;
        }

        public async Task<Course> InsertAsync(CourseInsertRequestViewModel request)
        {
            Course course = new Course
            {
                Code = request.Code,
                Name = request.Name,
                Credit = request.Credit
            };
            await unitOfWork.CourseRepository.CreateAsync(course);

            if (await unitOfWork.CourseRepository.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException($"Course Insertion Operation Unsuccessful");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (course == null)
                return true;

            return false;
        }

        public async Task<bool> IsIdExists(int courseId)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == courseId);
            if (course == null)
                return false;

            return true;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == name);
            if (course == null)
                return true;

            return false;
        }

        public async Task<bool> IsCourseIdExists(int id)
        {
            var course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == id);
            if (course == null)
                return false;

            return true;
        }

        public async Task<Course> UpdateAsync(string code, Course course)
        {
            var _course = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (_course == null)
                throw new ApplicationValidationException("Course Not Found");

            if (!string.IsNullOrWhiteSpace(course.Code))
            {
                var existsAlreadyCode = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
                if (existsAlreadyCode != null)
                    throw new ApplicationValidationException("Updated Code Already Exists");

                _course.Code = course.Code;
            }

            if (!string.IsNullOrWhiteSpace(course.Name))
            {
                var existsAlreadyName = await unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == course.Name);
                if (existsAlreadyName != null)
                    throw new ApplicationValidationException("Updated Name Already Exists");

                _course.Name = course.Name;
            }

            unitOfWork.CourseRepository.UpdateAsync(_course);
            if (await unitOfWork.CourseRepository.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("An Error Occured Updating Data");
        }
    }
}