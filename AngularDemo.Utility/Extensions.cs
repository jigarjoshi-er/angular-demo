using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel;

namespace AngularDemo.Utility
{
    public static class Extensions
    {
        public static Guid GetUserID(this IIdentity identity)
        {
            string userId = identity.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Guid.Empty;
            }

            Guid id;

            if(Guid.TryParse(userId, out id))
            {
                return id;
            }

            return Guid.Empty;
        }
    }

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
