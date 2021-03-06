using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using API.DLL.Repositories;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BLL.Services
{
    public interface ITestService
    {
        Task DummyData();
        Task AddRoles();
        Task AddUsersWithRoles();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public TestService(IUnitOfWork unitOfWork, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddRoles()
        {
            var roles = new List<string>{
                "Admin",
                "Manager",
                "SuperAdmin"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }

        public async Task AddUsersWithRoles()
        {
            var users = new List<ApplicationUser>{
                new ApplicationUser{Email = "gokberk@gokberk.com", UserName = "gokberk@gokberk.com"},
                new ApplicationUser{Email = "gizem@gizem.com", UserName = "gizem@gizem.com"}
            };

            foreach (var user in users)
            {
                if (!context.Users.Any(x => x.Email == user.Email))
                {
                    var result = await userManager.CreateAsync(user, "Qwerty123!.");
                    if (result.Succeeded && user.Email.Contains("gokberk"))
                    {
                        await userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                    else if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

        public async Task DummyData()
        {
            // var studentsDummy = new Faker<Student>()
            //         .RuleFor(x => x.Name, f => f.Name.FullName())
            //         .RuleFor(x => x.Email, (f, s) => f.Internet.Email(s.Name));

            // var departmentsDummy = new Faker<Department>()
            //         .RuleFor(x => x.Name, f => f.Name.FirstName())
            //         .RuleFor(x => x.Code, f => f.Lorem.Random.Uuid().ToString())
            //         .RuleFor(x => x.Students, f => studentsDummy.Generate(50).ToList());

            // var departmentListWithStudent = departmentsDummy.Generate(100).ToList();

            // await context.Departments.AddRangeAsync(departmentListWithStudent);
            // await context.SaveChangesAsync();

            // var coursesDummy = new Faker<Course>()
            //         .RuleFor(x => x.Name, f => f.Name.FirstName())
            //         .RuleFor(x => x.Code, f => f.Lorem.Random.Uuid().ToString())
            //         .RuleFor(x => x.Credit, f => f.Random.Number(1, 10));

            // var courses = coursesDummy.Generate(50).ToList();
            // await context.Courses.AddRangeAsync(courses);
            // await context.SaveChangesAsync();

            // var studentIds = await context.Students.Select(x => x.StudentId).ToListAsync();
            // var courseIds = await context.Courses.Select(x => x.CourseId).ToListAsync();

            // var courseStudentDummy = new Faker<CourseStudent>()
            //         .RuleFor(x => x.CourseId, f => f.PickRandom<int>(courseIds))
            //         .RuleFor(x => x.StudentId, f => f.PickRandom<int>(studentIds));

            // var enrollments = courseStudentDummy.Generate(50).ToList();

            // await context.CourseStudents.AddRangeAsync(enrollments);
            // await context.SaveChangesAsync();

            await Task.FromResult(string.Empty);
        }
    }
}