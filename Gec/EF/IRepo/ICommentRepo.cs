using Gec.Models.Gec;
using System.Collections.Generic;

namespace Gec.EF.IRepo
{
    public interface ICommentRepo
    {
        void Add(Comment comment);


        void Delete(int id);

        Comment Get(int id);

        ICollection<Comment> GetAll();

         Comment Update(Comment comment);
       
    }
}