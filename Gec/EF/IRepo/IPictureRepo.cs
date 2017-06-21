using Gec.Models.Gec;
using System.Collections.Generic;

namespace Gec.EF.IRepo
{
    public interface IPictureRepo
    {
        void Add(Picture comment);

        void Delete(int id);

        Picture Get(int id);

        ICollection<Picture> GetAll();


        Picture Update(Picture comment);

    }
}