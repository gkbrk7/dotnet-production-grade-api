using System.Collections.Generic;
using API.DLL.Models;

namespace API.DLL.ResponseViewModels
{
    public class StudentCourseViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}