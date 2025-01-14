using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseWork1.Models;


namespace CourseWork1.Services
{
    public class TransactionsService : ITransactions
    {
        private readonly string transactionFilePath = Path.Combine(AppContext.BaseDirectory, "Transactions.json");

        
        public async Task AddTransactionsAsync(Transaction transaction) //to add transaction to transaction json
        {
            try
            {
                //getting all the transactions and inserting it into transactions variable
                var transactions = await GetTansactionAsync();
                //setting the transactionid based on the existing transactions
                transaction.TransactionId = transactions.Count > 0 ? transactions.Max(t => t.TransactionId) + 1 : 1;
                //adding the new transaction into the list of existing transactions
                transactions.Add(transaction);
                //saving the updated transaction list into the json file
                await SaveTransactionAsync(transactions);
            }
            catch (Exception ex) 
            {
                //for debugging
                Console.WriteLine($"Error adding task in AddTransactionsAsync: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
            
        }

        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId) 
        {
            try
            {
                var transacations = await GetTansactionAsync();
                //if there are no transactions in  then, new list is returened, 
                //if transactions with the user id is returned. 
                return (transacations ?? new List<Transaction>()).Where(t => t.User_id == userId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transactions for user {userId}: {ex.Message}");
                throw;

            }
        }
        

        public async Task<List<Transaction>> GetTansactionAsync()
        {
            try
            {
                if (!File.Exists(transactionFilePath))
                {
                    //if a transaction file doesnt exist then an emply list is saved
                    var emptyList = new List<Transaction>();
                    return emptyList;
                }
                var json = await File.ReadAllTextAsync(transactionFilePath);
                //if a json file exists then, it is saved in var jason

                return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
                // the json file is not null then it is deserialized and returned as a list
                // else if the json file is null then a new list is returned
            }
            catch (JsonException jsonEx) //if a problem arises while deserializing the json file
            {
                Console.WriteLine($"A Error has been detcted during deserialization in transactionservice :{jsonEx.Message}");
                throw;
            }
            catch (IOException ioEx) // if a error arises in I/O of transaction service
            {
                Console.WriteLine($"I/O error has occured in transactionservice:{ioEx.Message}");
                throw;
            }
            catch (Exception ex)  // for when an unpected error occurs
            {
                Console.WriteLine($"Unecpected error has occured in transactionservice: {ex.Message}");
                throw;
            }
        }

        private async Task SaveTransactionAsync(List<Transaction> transactions) // to save a transaction list into json file
        {
            try
            {
                //  changes transaction list into json format by serializing it
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
                //this is used to write the contents of the transaction json into the json file in usersFilePath i.e. UserDatas.json
                await File.WriteAllTextAsync(transactionFilePath, json);
            }
            catch (Exception ex)
            {
                //writing the error that occured in console app
                Console.WriteLine($"Error saving tasks: {ex.Message}");
                throw; 
            }
        }



    }
}
