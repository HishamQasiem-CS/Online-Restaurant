using FoodApplication.Data;
using FoodApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Controllers
{
	public class RecipeController : Controller
	{
		private AppDbContext _db;
		public RecipeController(AppDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index(string search)
		{
			var categore = await _db.categoreys.Include(x => x.Products).ToListAsync();
         
            if (!String.IsNullOrEmpty(search))
            {
                categore= categore.Where(x=>x.CategoreyName.Contains(search) ).ToList();
            }
			return View(categore);
		}
        
		public IActionResult Details(int? id)
		{
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var product = _db.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);

        }
        public IActionResult Search(string searchQuery)
        {
            var products = _db.products.Where(p => p.ProductName.Contains(searchQuery)).ToList();
            return View(products);
        }


    }
}
