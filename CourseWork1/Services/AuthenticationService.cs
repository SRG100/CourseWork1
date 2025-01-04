using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork1.Models;

namespace CourseWork1.Services
{
    public class AuthenticationService
    {
        //declaring a private 
        private User currentUser;

        public User GetCurrentUser() //method to get the variable "currentUser"
        {
            return currentUser;
        }

        public void SetCurrentUser(User user) //method to insert value into "currentUser"
        {
            currentUser = user;
        }

        public bool IsAuthenticated() //to get if a user has log in 
        {
            if (currentUser == null)
            { 
                return false;
            }
            return true;
        }

        public void Logout() //method to remove currentUser
        { 
            currentUser = null;
        }

    }
}
