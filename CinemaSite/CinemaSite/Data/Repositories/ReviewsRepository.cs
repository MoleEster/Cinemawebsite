using CinemaSite.Data.Database;
using CinemaSite.Data.Interfaces;
using CinemaSite.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Repositories
{
    public class ReviewsRepository : IAllReviews
    {
        private readonly AppDbContext _context;

        public ReviewsRepository(AppDbContext context)
        {
            _context = context;
        }
        IEnumerable<Review> IAllReviews.GetAllReviews => _context.Reviews;

        void IAllReviews.AddNewReview(Guid userId, Review review)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            List<Review> userReviews = new List<Review>();
            if (user.Reviews != null)
            {
                foreach (string item in user.Reviews.Split(','))
                {
                    userReviews.Add(_context.Reviews.FirstOrDefault(r => r.Id.ToString() == item));
                }
                if (review != null && user != null)
                {
                    if (userReviews.Where(r => review.Film == r.Film) == null)
                    {
                        userReviews.Add(review);
                    }
                    else
                    {
                        _context.Reviews.Remove(_context.Reviews.First(r => string.Equals(r.Film, review.Film)));
                        userReviews.Remove(userReviews.First(r => review.Film == r.Film));
                        userReviews.Add(review);
                    }
                }
            }
            else
            {
                userReviews.Add(review);
                user.Reviews = $"{review.Id}";
            }
            user.Reviews = String.Join(',', userReviews.Select(r => r.Id));
            _context.Reviews.Add(review);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
