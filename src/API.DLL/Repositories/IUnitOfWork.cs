using System;
using System.Threading.Tasks;
using API.DLL.Contexts;

namespace API.DLL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
        ITransactionHistoryRepository TransactionHistoryRepository { get; }
        ICustomerBalanceRepository CustomerBalanceRepository { get; }

        Task<bool> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private readonly ApplicationDbContext context;
        private IDepartmentRepository _departmentRepository;
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;
        private ICourseStudentRepository _courseStudentRepository;
        private TransactionHistoryRepository _transactionHistoryRepository;
        private CustomerBalanceRepository _customerBalanceRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository ?? new DepartmentRepository(context);
        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(context);
        public ICourseRepository CourseRepository => _courseRepository ?? new CourseRepository(context);
        public ITransactionHistoryRepository TransactionHistoryRepository => _transactionHistoryRepository ?? new TransactionHistoryRepository(context);
        public ICustomerBalanceRepository CustomerBalanceRepository => _customerBalanceRepository ?? new CustomerBalanceRepository(context);
        public ICourseStudentRepository CourseStudentRepository => _courseStudentRepository ?? new CourseStudentRepository(context);

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}