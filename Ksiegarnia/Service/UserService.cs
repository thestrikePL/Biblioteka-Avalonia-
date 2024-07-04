using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Service
{
    public class UserService
    {
        private readonly IAppDbContext _appDbContext;

        public UserService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _appDbContext.Users;
        }

        public User GetUser(string name)
        {
            return _appDbContext.Users.First(u => u.Name == name);
        } 

        public async Task<User> CreateUser(string name, string password) 
        {
            var newUser = new User()
            {
                Name = name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };
            var result = _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> RemoveUser(int id)
        {
            var userToRemove = _appDbContext.Users.First(u => u.Id == id);

            var res = _appDbContext.Users.Remove(userToRemove);
            await _appDbContext.SaveChangesAsync();
            return res != null;
        }

    }
}
