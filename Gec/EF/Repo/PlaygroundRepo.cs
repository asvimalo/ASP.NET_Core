using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gec.EF.Db;

namespace Gec.EF.Repo
{
    public class PlaygroundRepo
    {
        private GecContext _ctx;
        public PlaygroundRepo(GecContext ctx)
        {
            _ctx = ctx;
        }
    }
}
