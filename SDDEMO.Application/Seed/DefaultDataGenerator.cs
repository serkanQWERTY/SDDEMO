using Microsoft.AspNetCore.Identity;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DefaultDataGenerator.cs
{
    public class DefaultDataGenerator
    {
        public static User DefaultUser()
        {
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(null, "serkan"); 

            return new User
            {
                id = Guid.Parse(GuidValues.StaticUserId.ToDescriptionString()),
                name = "admin",
                surname = "admin",
                username = "admin",
                passwordHash = hashedPassword, 
                mailAddress = "admin@example.com",
                creationDate = DateTime.UtcNow,
                isActive = true,
                isDeleted = false,
                updatedDate = DateTime.UtcNow
            };
        }
    }
}
