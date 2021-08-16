using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using CinemaSite.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CinemaSite.Controllers
{
    public class AddNewFilmController:Controller
    {
        private readonly IAllGenres _allGenres;
        private readonly IAllUsers _allUsers;
        private readonly IAllFilms _allFilms;
        private readonly IHostingEnvironment _enviroment;
        private User activeUser;
        public AddNewFilmController(IAllGenres Genres, IAllUsers users, IAllFilms films, IHostingEnvironment enviroment)
        {
            _allFilms = films;
            _allGenres = Genres;
            _enviroment = enviroment;
            _allUsers = users;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var activeUserId = Request.Cookies["ActiveUser"];
            activeUser = _allUsers.GetAllUsers.First(u => u.Id.ToString() == activeUserId.ToString());
            var view = new AddNewFilmViewModel
            {
                ActiveUser = activeUser,
                Genres = _allGenres.GetAllGenres.ToList()
            };
            return View(view);
        }
        [HttpPost]
        public ActionResult AddFilm(string name,string description,string rating,string genres,IFormFile files)
        {
            if (HttpContext.Request.Form.Files != null)
            {
                genres = genres.ToLower();
                string PathDB = string.Empty;
                var newFileName = string.Empty;
                var fileName = string.Empty;
                var newfiles = HttpContext.Request.Form.Files;
                foreach (var file in newfiles)
                {
                    if(file.Length > 0)
                    {
                        name = String.Concat(name.Where(l => !Char.IsWhiteSpace(l)));
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var FileExtension = Path.GetExtension(fileName);
                        newFileName = name + FileExtension;
                        fileName = Path.Combine(_enviroment.WebRootPath, "img") + $@"\{newFileName}";
                        PathDB = "img/" + fileName;
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        double Rateing = Math.Round(double.Parse(rating));
                        Math.Round(Rateing,2);
                        _allFilms.AddNewFilm(name, description, Rateing, genres, $"/img/{newFileName}");
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
