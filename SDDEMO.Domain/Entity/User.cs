using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Domain.Entity
{
    public class User : BaseEntity
    {
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mailAddress { get; set; }
        public string passwordHash { get; set; }

        public void SetPassword(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            passwordHash = passwordHasher.HashPassword(this, password);
        }

        public bool VerifyPassword(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(this, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
