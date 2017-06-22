using Gec.EF.Db;
using Gec.EF.IRepo;
using Gec.Models.Gec;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.EF.Repo
{
    public class FeedRepo : IFeedRepo
    {
        private GecContext _ctx;

        public FeedRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Feed feed)
        {
            if(feed != null)
            {
               
                    _ctx.Feeds.Add(feed);
                    _ctx.SaveChanges();
               
            }
        }
        public void Delete(int id)
        {
           
                var delete = _ctx.Feeds.FirstOrDefault(d => d.FeedId == id);
                _ctx.Feeds.Remove(delete);
                _ctx.SaveChanges();
           
        }
        public Feed Update(Feed feed)
        {
            if (feed != null)
            {
               
                    _ctx.Entry(feed).State = EntityState.Modified;
                    _ctx.SaveChangesAsync();                    
                         
            }
            return feed;
        }
        public Feed Get(int id)
        {
           
                return _ctx.Feeds.Include(f => f.User)
                    .Include(f => f.Comments)
                    .FirstOrDefault(f => f.FeedId == id);
            
        }
        public ICollection<Feed> GetAll(string feedType)
        {
            
                return _ctx.Feeds.Where(f => f.FeedType == feedType).ToList();
           
        }
        
    }
}
