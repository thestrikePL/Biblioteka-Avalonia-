using Ksiegarnia.DTO;
using Ksiegarnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Service
{
    public class AuthService
    {
        private readonly UserService _userService;

        public AuthService(UserService userService) 
        {
            _userService = userService;
        }

        public User Login(LoginRequest loginRequest)
        {
            var user = _userService.GetUser(loginRequest.Username);
            if(user == null)
            {
                return null;
            }

            if(BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }
    }
}
