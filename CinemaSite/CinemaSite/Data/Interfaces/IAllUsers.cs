using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Interfaces
{
    public interface IAllUsers
    {
        public IEnumerable<User> GetAllUsers { get; }
        public bool AddNewUser(string Name, string Password, string Email);
        public void UpdateUser(User user, string name, string password, string email);
    }
}
