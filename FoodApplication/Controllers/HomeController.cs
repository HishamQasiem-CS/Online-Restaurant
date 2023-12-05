using FoodApplication.Data;
using FoodApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodApplication.Controllers
{
    public class HomeController : Controller
    {
		private AppDbContext _db;
		public HomeController(AppDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
        {
			var categore = await _db.products.ToListAsync();
			
            return View(categore);
        }

        public IActionResult Book_Table()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Book_Table(Book_Table book_Table)
        {
            _db.book_Tables.Add(book_Table);
            _db.SaveChanges();
            return RedirectToAction("Message");
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            _db.contacts.Add(contact);
            _db.SaveChanges();
            return RedirectToAction("Message");
            
        }
        public IActionResult Message()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}