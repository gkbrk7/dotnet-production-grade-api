using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using API.DLL.ResponseViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId)
        {
            return await context.Students.Include(x => x.CourseStudents).ThenInclude(x => x.Course).Select(x => new StudentCourseViewModel
            {
                StudentId = x.StudentId,
                CreatedAt = x.CreatedAt.ToString(),
                Name = x.Name,
                Email = x.Email,
                Courses = x.CourseStudents.Select(x => x.Course).ToList(),
                UpdatedAt = x.LastUpdatedAt.ToString()
            }).FirstOrDefaultAsync(x => x.StudentId == studentId);
        }
    }
}