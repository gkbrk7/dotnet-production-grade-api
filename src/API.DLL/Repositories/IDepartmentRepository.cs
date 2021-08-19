using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
    }

    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
