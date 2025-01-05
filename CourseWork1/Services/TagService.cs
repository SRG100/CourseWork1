using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using CourseWork1.Models;

namespace CourseWork1.Services
{
    public class TagService:ITags

    {
        private readonly string tagsFilePath = Path.Combine(AppContext.BaseDirectory, "Tags.json");

        public async Task AddTagAsync(Tags tag) 
        {
            try
            {
                //getting all the transactions and inserting it into transactions variable
                var tags = await GetTagsAsync();
                //adding the new transaction into the list of existing transactions
                tags.Add(tag);
                //saving the updated tags list into the json file
                await SaveTagsAsync(tags);
            }
            catch (Exception ex)
            {
                //for debugging
                Console.WriteLine($"Error adding task in AddTransactionsAsync: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }
        public async Task<List<Tags>> GetTagsAsync() 
        {
            try
            {
                if (!File.Exists(tagsFilePath))
                {
                    //if a tags file doesnt exist then an emply list is saved
                    var emptyList = new List<Tags>();
                    return emptyList;
                }
                var json = await File.ReadAllTextAsync(tagsFilePath);
                //if a json file exists then, it is saved in var jason
                return JsonSerializer.Deserialize<List<Tags>>(json) ?? new List<Tags>();
                // the json file is not null then it is deserialized and returned as a list
                // else if the json file is null then a new list is returned
            }
            catch (JsonException jsonEx) //if a problem arises while deserializing the json file
            {
                Console.WriteLine($"A Error has been detcted during deserialization in tagsService :{jsonEx.Message}");
                throw;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error has occured in tagsService:{ioEx.Message}");
                throw;
            }
            catch (Exception ex)  // for when an unpected error occurs
            {
                Console.WriteLine($"Unecpected error has occured in tagsService: {ex.Message}");
                throw;
            }
        }

        public async Task SaveTagsAsync(List<Tags> tags)
        {
            try
            {
                //  changes tags list into json format by serializing it
                var json = JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });
                //this is used to write the contents of the tags json into the json file in tagsFilepath i.e. Tags.json
                await File.WriteAllTextAsync(tagsFilePath, json);
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
