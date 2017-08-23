using Gec.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Models.Gec
{
    
    public class Feed : IComparable<Feed>, IEquatable<Feed>
    {
        public int FeedId { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Article { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Archived { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public int? Id { get; set; }
        public User User { get; set; }
        public bool IsArchived { get; set; }
        public string FeedType { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        public int CompareTo(Feed feed)
        {
            int result = DateCreated.CompareTo(feed.DateCreated);
            if (result == 0)
            {
                result = Stars.CompareTo(feed.Stars);
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Feed);
        }
        public bool Equals(Feed feed)
        {
            if (feed == null)
            {
                return false;
            }
            if (this.Id == feed.Id)
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
                return ((Name != null ? Name.GetHashCode() : 0));  
            }
        }
    }
}
