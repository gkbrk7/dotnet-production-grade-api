using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> InsertAsync(Student student);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetAsync(string email);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.Email == email);
            context.Entry(student).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await context.Students.ToListAsync();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await context.Students.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Student> InsertAsync(Student student)
        {
            await context.AddAsync(student);
            await context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var _student = await context.Students.FirstOrDefaultAsync(x => x.Email == email);
            _student.Name = student.Name;
            context.Entry(_student).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return _student;
        }
    }
}