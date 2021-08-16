using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Models
{
    public class Genre
    {
        public Genre(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
