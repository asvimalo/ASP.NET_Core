using Gec.Models.Account;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gec.Models.Gec
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int  PictureId { get; set; }
        public int Id { get; set; }
        public Picture Picture { get; set; }
        public User User { get; set; }
    }
}
