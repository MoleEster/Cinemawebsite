using CinemaSite.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Interfaces
{
    public interface IAllFilms
    {
        public IEnumerable<Film> GetAllFilms { get; }
        public void AddNewFilm(string name, string description, double rating, string genres, string img);
    }
}
