using FoodApplication.Data;
using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Drawing.Design;
using System.Net;

namespace FoodApplication.Controllers
{
    public class DashboardController : Controller
    {
        private AppDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(AppDbContext db, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DisplayCategores()
        {
            var categore = await _db.categoreys.Include(x => x.Products).ToListAsync();

            return View(categore);
        }
        public async Task<IActionResult> DisplayProducts()
        {
            var product = await _db.products.ToListAsync();

            return View(product);
        }

        public IActionResult AddCategore()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategore(Categorey categorey)
        {
            _db.categoreys.Add(categorey);
            _db.SaveChanges();
            return RedirectToAction("DisplayCategores");
        }
        public IActionResult AddProduct()
        {
            var ee = _db.categoreys.ToList();
            ViewBag.e = new SelectList(ee, "CategoreyId", "CategoreyName");
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            
            _db.products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }

        public IActionResult DeleteCategore(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var categore = _db.categoreys.Find(id);
            if (categore == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(categore);
        }
        [HttpPost]
        public IActionResult DeleteCategore(Categorey categorey)
        {
            _db.categoreys.Remove(categorey);
            _db.SaveChanges();
            return RedirectToAction("DisplayCategores");

        }


        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var pro = _db.products.Find(id);
            if (pro == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
           
            return View(pro);
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            _db.products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("DisplayProducts");

        }

        public IActionResult UpdateCategore(int? iidd)
        {
            if (iidd == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var categore = _db.categoreys.Find(iidd);
            if (categore == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View(categore);
        }
        [HttpPost]
        public IActionResult UpdateCategore(Categorey model)
        {

            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("DisplayCategores");
        }


        public IActionResult UpdateProduect(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var pro = _db.products.Find(id);
            if (pro == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var ee = _db.categoreys.ToList();
            ViewBag.e = new SelectList(ee, "CategoreyId", "CategoreyName");

            return View(pro);
        }
        [HttpPost]
        
        public  IActionResult UpdateProduect(Product model)
        {
            
           

            _db.Update(model);
            _db.SaveChanges();
           return RedirectToAction("DisplayProducts");
             
            
           
        }




        public async Task<IActionResult> DisplayUsers()
        {
            var user = await _db.Users.ToListAsync();

            return View(user);
        }



        public IActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewUser(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = register.Name,
                    Email = register.Email,
                    Address = register.Address,
                    Age = register.Age,
                    Country = register.Country,
                    City = register.City,
                    BirthDate = register.BirthDate,
                    Geunder = register.Geunder,



                };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, register.Password, false, false);
                    return RedirectToAction("DisplayUsers");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(register);
        }



        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("DisplayUsers");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // قم بتمرير نموذج المستخدم إلى صفحة التعديل
            return View(new EditUserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Address = user.Address,
                Age = user.Age,
                Country = user.Country,
                City = user.City,
                Geunder = user.Geunder,
                BirthDate = user.BirthDate,

                // يمكنك إضافة المزيد من الحقول حسب الحاجة
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                // قم بتحديث معلومات المستخدم
                user.UserName = model.Name;
                user.Email = model.Email;
                user.Address = model.Address;
                user.Age = model.Age;
                user.Country = model.Country;
                user.City = model.City;
                user.Geunder = model.Geunder;
                user.BirthDate = model.BirthDate;
                // قم بتحديث المزيد من الحقول حسب الحاجة

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // تم تحديث المعلومات بنجاح
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // إذا كان هناك أخطاء في النموذج، عد إلى صفحة التعديل مع البيانات المدخلة
            return View(model);
        }
        public IActionResult ViewNotesCustomer()
        {
            return View(_db.contacts.ToList());
        }
        public IActionResult ViewBppkTable()
        {
            return View(_db.book_Tables.ToList());
        }

        public IActionResult ResponseCoustomer(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var product = _db.contacts.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        public IActionResult Message()
        {
            return View();
        }

        public IActionResult AgreeOrNotBook(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            var pro = _db.book_Tables.Find(id);
            if (pro == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            

            return View(pro);
        }
        [HttpPost]
        public IActionResult AgreeOrNotBook(Book_Table model)
        {
            _db.book_Tables.Update(model);
            _db.SaveChanges();
            return RedirectToAction("ViewBppkTable");
        }
    }
     



}
    
    
