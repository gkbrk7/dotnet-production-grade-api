using System.Collections.Generic;
using System.Threading.Tasks;
using API.DLL.Contexts;
using API.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DLL.Repositories
{
    public interface ITransactionHistoryRepository : IRepositoryBase<TransactionHistory>
    {
    }

    public class TransactionHistoryRepository : RepositoryBase<TransactionHistory>, ITransactionHistoryRepository
    {
        private readonly ApplicationDbContext context;

        public TransactionHistoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
