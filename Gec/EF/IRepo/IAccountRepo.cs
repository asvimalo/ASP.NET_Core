using Gec.Models.Account;
using System.Collections.Generic;

namespace Gec.EF.Repo
{
    public interface IAccountRepo
    {
        void Add(User user);

        void Delete(int id);

        User Update(User user);

        User Get(int id);

        List<User> GetAll(string feedType);
    }
}