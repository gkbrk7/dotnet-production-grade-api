using System.Collections.Generic;
using System.Threading.Tasks;
using API.BLL.Utilities.Exceptions;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetAsync(string email);
        Task<Student> InsertAsync(Student student);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await studentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student Not Found.");
            }

            studentRepository.DeleteAsync(dbStudent);

            if (await studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("An Error Occured Deleting Data");
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await studentRepository.GetList();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await studentRepository.FindSingleAsync(x => x.Email == email);
        }

        public async Task<Student> InsertAsync(Student student)
        {
            await studentRepository.CreateAsync(student);
            if (await studentRepository.SaveCompletedAsync())
            {
                return student;
            }
            throw new ApplicationValidationException($"Student Insertion Operation Unsuccessful");
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await studentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student Not Found.");
            }

            dbStudent.Name = student.Name;
            studentRepository.UpdateAsync(dbStudent);

            if (await studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("An Error Occured Updating Data");
        }
    }
}