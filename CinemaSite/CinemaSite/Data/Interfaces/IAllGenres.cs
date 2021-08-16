using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Interfaces
{
    public interface IAllGenres
    {
        public IEnumerable<Genre> GetAllGenres { get;}
    }
}
