using CourseWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork1.Services
{
    public interface IUserService
    {
        //to register user
        Task SaveUserAsync(User user);
        //to view users
        Task<List<User>> GetAllUsersAsync();
    }
}
