using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.ViewModels
{
    public class FilmViewModel
    {
        public Film Film { get; set; }
        public Dictionary<User,Review> FilmReviews { get; set; }
        public User activeUser { get; set; }
    }
}
