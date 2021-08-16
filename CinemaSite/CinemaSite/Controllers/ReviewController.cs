using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IAllUsers _allUsers;
        private readonly IAllReviews _allReviews;
        private readonly IAllFilms _allFilms;

        public ReviewController(IAllUsers users,IAllFilms films,IAllReviews reviews)
        {
            _allUsers = users;
            _allFilms = films;
            _allReviews = reviews;
        }
        [HttpPost]
        public ActionResult AddReview(string title,string rating,string text,string filmName)
        {
            User activeUser = _allUsers.GetAllUsers.First(u => u.Id.ToString() == Request.Cookies["ActiveUser"].ToString());
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(rating) && !string.IsNullOrEmpty(text))
            {
                Review review = new Review()
                {
                    Id = Guid.NewGuid(),
                    Rating = Double.Parse(rating),
                    Text = text,
                    Title = title,
                    User = activeUser.Id.ToString(),
                    Film = _allFilms.GetAllFilms.First(f => string.Equals(f.Name, filmName)).Id.ToString()
                };
                _allReviews.AddNewReview(activeUser.Id, review);
                return Ok();
            }
            else
            {
                return RedirectToAction("Film","Home",new { filmName });
            }
        }
    }
}
