using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface ICustomerBalanceRepository : IRepositoryBase<CustomerBalance>
    {
    }

    public class CustomerBalanceRepository : RepositoryBase<CustomerBalance>, ICustomerBalanceRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerBalanceRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
