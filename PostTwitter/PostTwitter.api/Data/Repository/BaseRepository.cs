using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostTwitter.api.Data;

namespace PostTwitter.api.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly PostTwitterContext _context;

        public BaseRepository (PostTwitterContext context)
        {
            _context = context;
        }
    }
}
