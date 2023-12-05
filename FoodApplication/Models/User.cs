using Microsoft.AspNetCore.Identity;

namespace FoodApplication.Models
{
    public class User:IdentityUser
    {
        public int Age { get; set; }
        public string? Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegestraionDate { get; set; } = DateTime.Now;
        public Country Country { get; set; }
        public City City { get; set; }
        public Geunder Geunder { get; set; }

        public int PhoneNumber { get; set; }
        public List<Product> ShoppingCart { get; set; }=new List<Product>();
    }
}
