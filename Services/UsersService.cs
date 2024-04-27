using Bits_API.Controllers;
using Bits_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bits_API.Services
{
    public class UsersService
    {
        private readonly BitsContext _bitsContext;

        public UsersService(BitsContext bitsContext)
        {
            _bitsContext = bitsContext;
        }


        // Create New User in the database
        public User CreateNewUser(User newUser)
        {

            bool isEmailExist = _bitsContext.user.Any(u => u.email.Equals(newUser.email));

            if (isEmailExist)
            {
                throw new InvalidOperationException("User Already exists - exception in CreateNewUser");
            }

            _bitsContext.user.Add(newUser);
            _bitsContext.SaveChanges();
            return newUser;

        }

        public User GetUserByEmailPassword(string email, string password)
        {
            var user = _bitsContext.user.SingleOrDefault(u => u.email.Equals(email) && u.password.Equals(password));

            return user;
        }

        public User GetUserById(int id)
        {
            var user = _bitsContext.user.SingleOrDefault(u => u.userId == id);
            
            return user;
        }
    }
}
