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
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "Please enter your real age.")]
        public int Age { get; set; }
        public string Ocupation { get; set; }
        public string Hobbies { get; set; }

    }
}
