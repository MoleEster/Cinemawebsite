using CinemaSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Database
{
    public class DbObjects
    {
        public static void Initial(AppDbContext appDbContext)
        {
            if (!appDbContext.Genres.Any())
            {
                appDbContext.Genres.AddRange(Genres.Select(c => c.Value));
            }
            if (!appDbContext.Films.Any())
            {
                appDbContext.Add(
                    new Film
                    {
                        Id = Guid.NewGuid(),
                        Name = "Большой лебовски",
                        Genres = $"{Genres["Comedy"]},{Genres["Crime"]}",
                        Rating = 7.8,
                        Description = "Two goons mistake 'the Dude' Lebowski for a millionaire Lebowski" +
                        " and urinate on his rug. Trying to recompense his rug from the wealthy Lebowski," +
                        " he gets entwined in an intricate kidnapping case.",
                        Img = "/img/BigLebowski.jpg"
                    }) ;
            }
            if (!appDbContext.Users.Any())
            {
                appDbContext.Add(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin",
                        Password = "qwerty123456",
                        IsAdmin = true,
                        Email = "staricmaggacet@gmail.com",
                    }
                    ) ;
            }
            appDbContext.SaveChanges();
        }

        private static Dictionary<string, Genre> _genre;
        public static Dictionary<string, Genre> Genres
        {
            get
            {
                if (_genre == null)
                {
                    var list = new Genre[]
                    {
                        new Genre("Action"), 
                        new Genre("Romance"),
                        new Genre("Musical"),
                        new Genre("Western"),
                        new Genre("Fiction"),
                        new Genre("Thriller"),
                        new Genre("War"),
                        new Genre("Romantic comedy"),
                        new Genre("Drama"),
                        new Genre("Martial arts"),
                        new Genre("Disaster"),
                        new Genre("Legal drama"),
                        new Genre("Dark Comedy"),
                        new Genre("Slasher"),
                        new Genre("Ducumentary"),
                        new Genre("Experimantal"),
                        new Genre("Music"),
                        new Genre("Noir"),
                        new Genre("Horror"),
                        new Genre("Monster"),
                        new Genre("Slapstick"),
                        new Genre("Found footage"),
                        new Genre("Revisionist vestern"),
                        new Genre("Animation"),
                        new Genre("Science fiction"),
                        new Genre("Fantasy"),
                        new Genre("Comedy"),
                        new Genre("Melodrama"),
                        new Genre("Biographical"),
                        new Genre("Crime"),
                        new Genre("Psychological thriller"),
                        new Genre("Splatter"),
                        new Genre("Mockumentary"),
                        new Genre("Historical Fiction"),
                        new Genre("Short"),
                        new Genre("Apocalyptic and post-apocalyptic fiction"),
                        new Genre("Gangster"),
                        new Genre("Epic"),
                        new Genre("Neo-noir"),


                    };
                    _genre = new Dictionary<string, Genre>();
                    foreach (Genre item in list)
                    {
                        _genre.Add(item.Name, item);
                    }
                }
                return _genre;
            }
        }
    }
}
