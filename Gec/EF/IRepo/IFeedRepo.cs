using Gec.Models.Gec;
using System.Collections.Generic;

namespace Gec.EF.IRepo
{
    public interface IFeedRepo
    {

        void Add(Feed feed);

        void Delete(int id);

        Feed Update(Feed feed);

        Feed Get(int id);

        ICollection<Feed> GetAll(string feedType);
       

    }
}