using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaSite.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Film> Films { get; set; }
        public User ActiveUser { get; set; }
    }
}
