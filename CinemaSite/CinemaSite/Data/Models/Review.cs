using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public string Film { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
    }
}
