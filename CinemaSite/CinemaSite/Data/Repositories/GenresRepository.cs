using CinemaSite.Data.Database;
using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Repositories
{
    public class GenresRepository : IAllGenres
    {
        private readonly AppDbContext _context;

        public GenresRepository(AppDbContext context)
        {
            _context = context;
        }
        IEnumerable<Genre> IAllGenres.GetAllGenres => _context.Genres.ToList();
    }
}
