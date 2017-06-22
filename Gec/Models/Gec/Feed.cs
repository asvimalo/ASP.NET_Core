using Gec.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Models.Gec
{
    
    public class Feed
    {
        public int FeedId { get; set; }
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
    }
}
