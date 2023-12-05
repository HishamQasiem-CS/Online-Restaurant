using FoodApplication.Data;
using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private AppDbContext _db;
        private UserManager<User> _userManager;
        public ShoppingCartController(AppDbContext db, UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
       
    }
}



