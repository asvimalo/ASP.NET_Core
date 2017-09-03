using Gec.Models.Account;
using Gec.Models.Gec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.ViewModels
{
    public class BlogViewModel
    {

        public int? Stars { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Article { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateArchived { get; set; }
        public int? PictureId { get; set; }
        public Picture Picture { get; set; }
        public int? Id { get; set; }
        public User User { get; set; }
        public bool? IsArchived { get; set; }
        public string FeedType { get; set; }
        //public int Likes { get; set; }
        //public int UnLikes { get; set; }
        //public ICollection<Comment> Comments { get; set; }
    }
}
