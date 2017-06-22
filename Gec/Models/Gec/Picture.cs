using Gec.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Gec.Models.Gec
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }
        public User User { get; set; }

        public ICollection<Feed> Feeds { get; set; }
    }
}
