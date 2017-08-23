using Gec.EF.Db;
using Gec.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.EF.Repo
{
    public class AccountRepo : IAccountRepo
    {
        GecContext _ctx;
        public AccountRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll(string feedType)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
