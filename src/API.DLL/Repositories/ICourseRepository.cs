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
    public interface ICourseRepository : IRepositoryBase<Course>
    {
    }

    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext context;

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
