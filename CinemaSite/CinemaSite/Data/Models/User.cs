using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSite.Data.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+",ErrorMessage ="Incorrect email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"[a-zA-Z0-9]{6,30}", ErrorMessage = "Password should be more then 6 symbols and not more then 30")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Reviews { get; set; }
    }
}
