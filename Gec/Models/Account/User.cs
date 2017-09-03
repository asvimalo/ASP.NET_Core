 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Models.Account
{
    public class User : IdentityUser , IEquatable<User>, IComparable<User>
    {
        public int CompareTo(User user)
        {
            int result = UserName.CompareTo(user.UserName);
            if (result == 0)
            {
                result = Email.CompareTo(user.Email);
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj as User);
        }
        public bool Equals(User user)
        {
            if (user == null)
            {
                return false;
            }
            if (this.UserName == user.UserName)
            {
                return true;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((UserName != null ? UserName.GetHashCode() : 0));
            }
        }
    }
}
