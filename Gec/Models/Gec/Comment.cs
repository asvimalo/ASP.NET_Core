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
        public DateTime DateCreated { get; set; }
        public int  PictureId { get; set; }
        public int Id { get; set; }
        public Picture Picture { get; set; }
        public User User { get; set; }

        public int CompareTo(Comment comment)
        {
            int result = DateCreated.CompareTo(comment.DateCreated);
            
            return result;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Comment);
        }
        public bool Equals(Comment comment)
        {
            if (comment == null)
            {
                return false;
            }
            if (this.Id == comment.Id)
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
                return ((CommentId != 0 ? CommentId.GetHashCode() : 0));
            }
        }
    }
}
