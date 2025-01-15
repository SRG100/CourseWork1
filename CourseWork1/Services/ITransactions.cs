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
        //to add transactions
        Task AddTransactionsAsync(Transaction transaction);
        //to get the transations based on user id
        Task <List<Transaction>> GetTransactionsByUserIdAsync(int userId);
        //getting the transactions 
        Task<List<Transaction>> GetTansactionAsync();

    }
}
