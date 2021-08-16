using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CinemaSite.ViewModels
{
    public class UserViewModel
    {
        public User ActiveUser { get; set; }
        public List<(Review,Film)> UsersReviews { get; set; }
    }
}
