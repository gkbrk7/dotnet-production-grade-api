using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await studentRepository.DeleteAsync(email);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await studentRepository.GetAllAsync();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await studentRepository.GetAsync(email);
        }

        public async Task<Student> InsertAsync(Student student)
        {
            return await studentRepository.InsertAsync(student);
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            return await studentRepository.UpdateAsync(email, student);
        }
    }
}