using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface ICourseStudentRepository : IRepositoryBase<CourseStudent>
    {
    }

    public class CourseStudentRepository : RepositoryBase<CourseStudent>, ICourseStudentRepository
    {
        private readonly ApplicationDbContext context;

        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
