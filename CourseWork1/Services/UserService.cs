using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using CourseWork1.Models;


namespace CourseWork1.Services
{
    public class UserService : IUserService //implementing the interface class IUserService
    {
        private readonly string usersFilePath = Path.Combine(AppContext.BaseDirectory, "UserDatas.json");

        public async Task SaveUserAsync(User user)
        {
            try
            {
                // loading all the existing users detail in list into users
                var users = await GetAllUsersAsync();

                //Autoincreasing user id
                if (users.Any()) //if users is emplty
                {
                    //the new user's id is assigned to be 1 more than the last value
                    user.UserId=users.Max(x => x.UserId)+1;
                }
                else
                {
                    //if no users  then, this is the new user is assignmed 1
                    user.UserId=1;
                }

                //hashing the password for extra security
                user.Password = HashPassword(user.Password);


                //adding the new user into the existing user list or new list(if no old user exist)
                users.Add(user);
                //using the method saveUserAsync to save the updated user list into the json file
                await SaveUserAsync(users);


            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Unexpected eoor at add user async () {ex.Message}");
                throw;
            }

        }

        // Loading all the existing user
        public async Task<List<User>> GetAllUsersAsync()
        {
            try //To handle exception
            {
                if (!File.Exists(usersFilePath)) //to get wheather a json file existis or not 
                {
                    //if json file already doen't exist then a new list is returned
                    return new List<User>();
                }
                var json = await File.ReadAllTextAsync(usersFilePath);
                //if a json file exists then, it is saved in var jason
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                // the json file is not null then it is deserialized and returned as a list
                // else if the json file is null then a new list is returned

            }
            catch (JsonException jsonEx) //if a problem arises while deserializing the json file
            {
                Console.WriteLine($"A Error has been detcted during deserialization in userservice :{jsonEx.Message}");
                return new List<User>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error has occured in userservice:{ioEx.Message}");
                return new List<User>();
            }
            catch (Exception ex)  // for when an unpected error occurs
            {
                Console.WriteLine($"Unecpected error has occured in userservice: {ex.Message}");
                return new List<User>();
            }
        }

        //to save the total user list into json file
        private async Task SaveUserAsync(List<User> users)
        {
            try
            {
                // this changes the list into json format by serializing it
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                //this is used to write the contents of the json into the json file in usersFilePath i.e. UserDatas.json
                await File.WriteAllTextAsync(usersFilePath, json);
            }
            catch (IOException ioEx) //to handle the IO ecception 
            {
                Console.WriteLine($"the I/O error occured while loading users: {ioEx.Message}");
                throw;
            }
            catch (Exception ex) //To handle unpected errors
            {
                Console.WriteLine($"Unexpected error while saving users: {ex.Message}");
                throw;
            }
        }

        //Hasing the password for security
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
