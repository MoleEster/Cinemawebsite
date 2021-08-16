using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Interfaces
{
    public interface IAllReviews
    {
        public IEnumerable<Review> GetAllReviews { get; }
        public void AddNewReview(Guid UserId, Review Review);
    }
}
