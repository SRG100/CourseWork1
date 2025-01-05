using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork1.Models;

namespace CourseWork1.Services
{
    public interface ITransactions
    {
        Task AddTransactionsAsync(Transaction transaction);
        Task<List<Transaction>> GetDebitByUserIdAsync(int userId);
        Task<List<Transaction>> GetCreditByUserIdAsync(int userId);
        Task<List<Transaction>> GetDebtByUserIdAsync(int userId);
        Task <List<Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<List<Transaction>> GetTansactionAsync();







    }
}
