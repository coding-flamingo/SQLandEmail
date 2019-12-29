using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLandEmailwithBlazorPage.Models
{
    public class PersonModel
    {
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public string Ocupation { get; set; }
        public string Hobbies { get; set; }

    }
}
