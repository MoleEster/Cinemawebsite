using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.ViewModels
{
    public class OtherUserViewModel
    {
        public User ThisUser { get; set; }
        public IEnumerable<(Review, Film)> Reviews { get; set; }
    }
}
