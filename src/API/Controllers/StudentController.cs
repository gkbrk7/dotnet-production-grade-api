using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BLL.Request;
using API.BLL.Services;
using API.DLL.Models;
using API.DLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await studentService.GetAllAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetAll(string email)
        {
            return Ok(await studentService.GetAsync(email));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(StudentInsertRequestViewModel studentRequest)
        {
            return Ok(await studentService.InsertAsync(studentRequest));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, Student student)
        {
            return Ok(await studentService.UpdateAsync(email, student));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            return Ok(await studentService.DeleteAsync(email));
        }
    }

    // For Testing Purposes
    // public static class StudentStatic
    // {
    //     private static List<Student> AllStudents { get; set; } = new List<Student>();
    //     public static Student InsertStudent(Student student)
    //     {
    //         AllStudents.Add(student);
    //         return student;
    //     }

    //     public static IEnumerable<Student> GetAllStudents()
    //     {
    //         return AllStudents;
    //     }

    //     public static Student GetStudent(string email)
    //     {
    //         return AllStudents.FirstOrDefault(x => x.Email == email);
    //     }

    //     public static Student UpdateStudent(string email, Student student)
    //     {
    //         var updated = AllStudents.Where(x => x.Email == email).FirstOrDefault();
    //         updated.Name = student.Name;
    //         return student;
    //     }

    //     public static Student DeleteStudent(string email)
    //     {
    //         var deleted = AllStudents.FirstOrDefault(x => x.Email == email);
    //         AllStudents.Remove(deleted);
    //         return deleted;
    //     }
    // }
}