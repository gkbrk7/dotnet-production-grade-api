using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}