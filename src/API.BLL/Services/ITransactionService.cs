using System;
using System.Threading.Tasks;
using API.DLL.Models;
using API.DLL.Repositories;

namespace API.BLL.Services
{
    public interface ITransactionService
    {
        Task FinancialTransaction();
    }

    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task FinancialTransaction()
        {
            var amount = new Random().Next(1000);
            var transaction = new TransactionHistory
            {
                Amount = amount
            };

            var customer = await unitOfWork.CustomerBalanceRepository.FindSingleAsync(x => x.Email == "gokberk@gokberk.com");
            customer.Balance += amount;

            await unitOfWork.TransactionHistoryRepository.CreateAsync(transaction);
            unitOfWork.CustomerBalanceRepository.UpdateAsync(customer);

            await unitOfWork.SaveChangesAsync();
        }
    }
}