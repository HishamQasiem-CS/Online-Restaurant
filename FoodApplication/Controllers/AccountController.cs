    using FoodApplication.Data;
using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Controllers
{
    
    public class AccountController : Controller
    {
        
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
         
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager=roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = register.Name,
                    Email = register.Email,
                    Address=register.Address,
                    Age= register.Age,
                    Country = register.Country,
                    City = register.City,
                    BirthDate = register.BirthDate,
                    Geunder= register.Geunder,
                    PhoneNumber=register.PhoneNumber,
                    
                    
                   
                };
                var result= await _userManager.CreateAsync(user,register.Password);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, register.Password, false, false);
                    return RedirectToAction("Login");
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if(ModelState.IsValid) {

                
                var result=await _signInManager.PasswordSignInAsync(model.Email,model.Password, false, lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    Console.WriteLine(result.ToString());   
                }
                    
                
            }
			return View(model);
		}
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



        [HttpGet]
        public async Task<IActionResult> EditProfile()
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
                Address= user.Address,
                Age= user.Age,
                Country= user.Country,
                City= user.City,
                Geunder= user.Geunder,
                BirthDate= user.BirthDate,

                // يمكنك إضافة المزيد من الحقول حسب الحاجة
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
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
                user.BirthDate= model.BirthDate;
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
        
    }
}
