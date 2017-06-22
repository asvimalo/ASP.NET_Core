
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.Models.Gec;
using Gec.EF.Db;
using Microsoft.EntityFrameworkCore;
using Gec.EF.IRepo;

namespace Gec.EF.Repo
{
    public class CommentRepo : ICommentRepo
    {
        private GecContext _ctx;

        public CommentRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Comment comment)
        {
            _ctx.Comments.Add(comment);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = _ctx.Comments.FirstOrDefault(c => c.CommentId == id);
            _ctx.Comments.Remove(delete);
            _ctx.SaveChanges();
        }

        public Comment Get(int id)
        {
            return _ctx.Comments.FirstOrDefault(c => c.CommentId == id);
        }

        public ICollection<Comment> GetAll()
        {
            return _ctx.Comments.ToList(); ;
        }

        
        public Comment Update(Comment comment)
        {
            _ctx.Entry(comment).State = EntityState.Modified;
            _ctx.SaveChangesAsync();
            return comment;
        }
    }
}
