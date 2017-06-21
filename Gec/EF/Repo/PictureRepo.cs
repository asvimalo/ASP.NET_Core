
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.Models.Gec;
using Gec.EF.Db;
using Microsoft.EntityFrameworkCore;

namespace Gec.EF.Repo
{
    public class PictureRepo
    {
        private GecContext _ctx;

        public PictureRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Picture foto)
        {
            _ctx.Pictures.Add(foto);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = _ctx.Pictures.FirstOrDefault(p => 
            p.PictureId == id);
            _ctx.Pictures.Remove(delete);
        }

        public Picture Get(int id)
        {
            return _ctx.Pictures.FirstOrDefault(p => p.PictureId == id);

        }

        public ICollection<Picture> GetAll()
        {
            return _ctx.Pictures.ToList();
        }

        public Picture Update(Picture picture)
        {
            _ctx.Entry(picture).State = EntityState.Modified;
            _ctx.SaveChangesAsync();
            return picture;
        }
    }
}
