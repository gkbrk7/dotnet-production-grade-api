using System;
using System.Threading.Tasks;
using API.DLL.Contexts;

namespace API.DLL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        Task<bool> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext context;
        private IDepartmentRepository _departmentRepository;
        private StudentRepository _studentRepository;
        private bool disposed = false;

        public IDepartmentRepository DepartmentRepository => _departmentRepository ?? new DepartmentRepository(context);
        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(context);
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await this.DisposeAsync();
        }
    }
}