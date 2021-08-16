using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using CinemaSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaSite.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {

        private readonly IAllUsers _allUsers;
        private readonly IAllReviews _allReviews;
        private readonly IAllFilms _allFilms;
        private User activeUser;
        public UserController(IAllUsers users, IAllReviews reviews, IAllFilms films)
        {
            _allUsers = users;
            _allReviews = reviews;
            _allFilms = films;
        }
        [HttpGet]
        [Route("/User/Registry")]
        public ActionResult Registry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {

                var User = _allUsers.GetAllUsers.First(c => c.Email.Equals(email));
                if (User != null && User.Password.Equals(password))
                {
                    Response.Cookies.Append("ActiveUser", User.Id.ToString());
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Response.Cookies.Delete("ActiveUser");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Registry(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.Email))
            {
                if (_allUsers.AddNewUser(user.Name, user.Password, user.Email))
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Registry");
        }
        public ActionResult Index()
        {
            List<(Review, Film)> UserReviews;
            UserViewModel ViewModel;
            var activeUserId = Request.Cookies["ActiveUser"];
            if (activeUserId != null)
            {
                activeUser = _allUsers.GetAllUsers.First(u => u.Id.ToString() == activeUserId.ToString());
                if (!string.IsNullOrEmpty(activeUser.Reviews))
                {
                    UserReviews = new List<(Review, Film)>();
                    (Review, Film) review;
                    foreach (string item in activeUser.Reviews.Split(',').ToArray())
                    {
                        review.Item1 = (_allReviews.GetAllReviews.First(r => r.Id.ToString() == item));
                        review.Item2 = (_allFilms.GetAllFilms.First(f => string.Equals(f.Id.ToString(), review.Item1.Film)));
                        UserReviews.Add(review);
                    }
                    ViewModel = new UserViewModel
                    {
                        ActiveUser = activeUser,
                        UsersReviews = UserReviews
                    };
                    return View(ViewModel);
                }
                ViewModel = new UserViewModel
                {
                    ActiveUser = activeUser
                };
                return View(ViewModel);
            }
            return View(new UserViewModel { });
        }

        [HttpPost]
        public ActionResult UpdateUser(string name,string email,string password)
        {
            var activeUserId = Request.Cookies["ActiveUser"];
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) )
            {
                _allUsers.UpdateUser(_allUsers.GetAllUsers.First(u => string.Equals(u.Id.ToString(), activeUserId.ToString())), name, password, email);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/User/{UserName}")]
        public ActionResult GetOtherUser(string UserName)
        {
            var activeUserId = Request.Cookies["ActiveUser"];
            User OtherUser = null;
            (Review, Film) review;
            List<(Review, Film)> thisUserReviews = new List<(Review, Film)>();
            var SearchingUser = _allUsers.GetAllUsers.Where(u => string.Equals(u.Name, UserName));
            if (SearchingUser != null && SearchingUser.Count() == 1)
            {
                OtherUser = _allUsers.GetAllUsers.First(u => string.Equals(u.Name, UserName));
                activeUser = _allUsers.GetAllUsers.First(u => string.Equals(u.Id.ToString(), activeUserId.ToString()));
                if (OtherUser.Id == activeUser.Id)
                    return RedirectToAction("Index");
                if(OtherUser.Reviews.Length !=0)
                foreach (string item in OtherUser.Reviews.Split(',').ToArray())
                {
                    review.Item1 = (_allReviews.GetAllReviews.First(r => r.Id.ToString() == item));
                    review.Item2 = (_allFilms.GetAllFilms.First(f => string.Equals(f.Id.ToString(), review.Item1.Film)));
                    thisUserReviews.Add(review);
                }
            }
            var view = new OtherUserViewModel
            {
                ThisUser = OtherUser,
                Reviews = thisUserReviews
            };
            return View(view);
        }
    }
}
