using CinemaSite.Data.Database;
using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Repositories
{
    internal class UsersRepository : IAllUsers
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        IEnumerable<User> IAllUsers.GetAllUsers => _context.Users.ToList();

        public void UpdateUser(User user, string name, string password, string email)
        {
            User ThisUser = _context.Users.First(u => string.Equals(u.Id.ToString(), user.Id.ToString()));
            if (!string.IsNullOrEmpty(password))
                ThisUser.Password = password;
            if (!string.IsNullOrEmpty(name))
                ThisUser.Name = name;
            if (!string.IsNullOrEmpty(email))
                ThisUser.Email = email;
        }

        bool IAllUsers.AddNewUser(string Name, string Password,string Email)
        {
            if(!(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Password)))
            {
                List<User> nigger = _context.Users.Where(u => u.Name == Name).ToList();
                if (_context.Users.Where(u => u.Name == Name).ToList().Count() == 0)
                {
                    User NewUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        Name = Name,
                        Email = Email,
                        Password = Password,
                        IsAdmin = false,
                        Reviews = null
                    };
                    _context.Users.Add(NewUser);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

    }
}
