using System.Collections.Generic;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Response;
using API.BLL.Utilities.Exceptions;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetAsync(string email);
        Task<StudentInsertResponseViewModel> InsertAsync(StudentInsertRequestViewModel studentRequest);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
        Task<bool> EmailExists(string email);
        Task<bool> IsIdExists(int studentId);
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student Not Found.");
            }

            unitOfWork.StudentRepository.DeleteAsync(dbStudent);

            if (await unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("An Error Occured Deleting Data");
        }

        public async Task<bool> EmailExists(string email)
        {
            var student = await unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (student != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsIdExists(int studentId)
        {
            var student = await unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == studentId);

            if (student != null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await unitOfWork.StudentRepository.GetList();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
        }

        public async Task<StudentInsertResponseViewModel> InsertAsync(StudentInsertRequestViewModel studentRequest)
        {
            var student = new Student
            {
                Email = studentRequest.Email,
                Name = studentRequest.Name,
                DepartmentId = studentRequest.DepartmentId
            };

            await unitOfWork.StudentRepository.CreateAsync(student);
            if (await unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return new StudentInsertResponseViewModel
                {
                    DepartmentId = student.DepartmentId,
                    Email = student.Email,
                    Name = student.Name,
                    StudentId = student.StudentId
                };
            }
            throw new ApplicationValidationException($"Student Insertion Operation Unsuccessful");
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student Not Found.");
            }

            dbStudent.Name = student.Name;
            unitOfWork.StudentRepository.UpdateAsync(dbStudent);

            if (await unitOfWork.StudentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("An Error Occured Updating Data");
        }
    }
}