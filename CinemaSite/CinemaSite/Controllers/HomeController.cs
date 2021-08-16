using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using CinemaSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaSite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IAllFilms _allFilms;
        private readonly IAllUsers _allUsers;
        private readonly IAllReviews _allReviews;
        private readonly IAllGenres _allGenres;
        private User activeUser;
        public HomeController(IAllFilms films,IAllUsers users,IAllReviews reviews, IAllGenres genres)
        {
            _allGenres = genres;
            _allFilms = films;
            _allUsers = users;
            _allReviews = reviews;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Request.Cookies.Count()!=0)
            if (Request.Cookies["ActiveUser"] != null)
            {
                activeUser = _allUsers.GetAllUsers.FirstOrDefault(u => u.Id.ToString() == Request.Cookies["ActiveUser"].ToString());
            }

            var films = new HomeViewModel
            {
                Films = _allFilms.GetAllFilms,
                Genres = _allGenres.GetAllGenres,
                ActiveUser = activeUser
            };
            return View(films);
        }
        [HttpGet]
        [Route("Home/{FilmName}")]
        public ActionResult Film(string FilmName)
        {
            Film film = _allFilms.GetAllFilms.First(f => string.Equals(f.Name, FilmName));
            Dictionary<User,Review> ReviewDictionary = new Dictionary<User, Review>();
            List<Review> FilmReviews = _allReviews.GetAllReviews.Where(r => string.Equals(r.Film, film.Id.ToString())).ToList();
            foreach (Review item in FilmReviews)
            {
                ReviewDictionary.Add(_allUsers.GetAllUsers.First(u => string.Equals(u.Id.ToString(), item.User)), item);
            }
            if (Request.Cookies["ActiveUser"] != null)
            {
                activeUser = _allUsers.GetAllUsers.FirstOrDefault(u => u.Id.ToString() == Request.Cookies["ActiveUser"].ToString());
            }
            var view = new FilmViewModel
            {
                Film = film,
                FilmReviews = ReviewDictionary,
                activeUser =activeUser
            };
            return View(view);
        }
        [HttpPost]
        [Route("Home/Search")]
        public ActionResult Search(string input)
        {
            List<Film> Selectedfilms = new List<Film>();
            if (Request.Cookies.Count() != 0)
            if (Request.Cookies["ActiveUser"] != null)
            {
                activeUser = _allUsers.GetAllUsers.FirstOrDefault(u => u.Id.ToString() == Request.Cookies["ActiveUser"].ToString());
            }
            if (string.IsNullOrEmpty(input))
            {
                Selectedfilms = _allFilms.GetAllFilms.ToList();
            }
            else if(_allGenres.GetAllGenres.Where(g=>string.Equals(g.Name,input)).Any())
            {
                Selectedfilms = _allFilms.GetAllFilms.Where(f => f.Genres.Split(',').Contains(input.ToLower())).ToList();
            }
            else
            {
                input = string.Concat(input.ToCharArray().Where(c => c != ' '));
                var SearchedFilm =  _allFilms.GetAllFilms.FirstOrDefault(f=> string.Equals(f.Name,input, StringComparison.OrdinalIgnoreCase));
                if (SearchedFilm != null)
                    Selectedfilms.Add(SearchedFilm);
            }
            var films = new HomeViewModel
            {
                Films = Selectedfilms,
                Genres = _allGenres.GetAllGenres,
                ActiveUser = activeUser
            };
            return PartialView("FilmsPartialView",films);
        }

    }
}
