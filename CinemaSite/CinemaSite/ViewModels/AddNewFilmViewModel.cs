using CinemaSite.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.ViewModels
{
    public class AddNewFilmViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public User ActiveUser { get; set; }
        public IFormFile img { get; set; }
    }
}
