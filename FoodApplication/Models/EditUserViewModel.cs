using System.ComponentModel.DataAnnotations;

namespace FoodApplication.Models
{
    public class EditUserViewModel
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime RegestraionDate { get; set; } = DateTime.Now;
        public Country Country { get; set; }
        public City City { get; set; }
        public Geunder Geunder { get; set; }

        public int PhoneNumber { get; set; }
    }
   
}

