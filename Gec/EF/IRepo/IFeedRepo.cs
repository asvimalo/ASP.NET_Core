﻿using Gec.Models.Gec;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gec.EF.IRepo
{
    public interface IFeedRepo
    {

        void Add(Feed feed);

        void Delete(int id);

        Feed Update(Feed feed);

        Feed Get(int id);

        List<Feed> GetAll(string feedType);
        Task<bool> SaveChangesAsync();
    }
}