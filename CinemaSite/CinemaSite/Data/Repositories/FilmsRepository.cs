using CinemaSite.Data.Database;
using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CinemaSite.Data.Repositories
{
    public class FilmsRepository : IAllFilms
    {
        private readonly AppDbContext _context;
        public FilmsRepository(AppDbContext context)
        {
            _context = context;
        }

        IEnumerable<Film> IAllFilms.GetAllFilms => _context.Films.ToList();

        public void AddNewFilm(string name, string description, double rating, string genres, string img)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description) && !double.IsNaN(rating) && genres.Any() && img != null)
            {
                Film film = new Film
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    Rating = rating,
                    Genres = genres,
                    Img = img
                };
                _context.Add(film);
                _context.SaveChanges();
            }
        }
    }
}
