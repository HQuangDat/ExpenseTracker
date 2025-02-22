using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountController(ApplicationDbContext db, IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        //This is the Login method

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User existUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existUser != null)
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(existUser, existUser.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, existUser.Email),
                        new Claim(ClaimTypes.Role, existUser.Roles.FirstOrDefault().RoleName),
                        new Claim(ClaimTypes.NameIdentifier, existUser.UserId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    TempData["success"] = "Login successful";
                    return RedirectToAction("List", "Expense");
                }
            }
            TempData["error"] = "Invalid email or password";
            return View();
        }

        //This is Logout method
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           return RedirectToAction("Login");
        }

        //This method will return the view for the Register page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user,user.PasswordHash);
                user.Roles.Add(_db.Roles.Find(2));
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User registered successfully";
                return RedirectToAction("Login");
            }

            TempData["error"] = "User registration failed, please check your information"; 
            return View(user);
        }

        //These two methods will edit the user in the database
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
                return NotFound();
            User user = _db.Users.Find(id);
            if(user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                TempData["success"] = "User updated successfully";
                return RedirectToAction("List");
            }
            return View(user);
        }

        //This method will delete the user from the database
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            User user = _db.Users.Find(id);
            if (user == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("List");
        }

        //This method will return the view for the list of users
        [HttpGet]
        public IActionResult List()
        {
            List<User> userList = _db.Users.ToList();
            return View(userList);
        }

        [HttpGet]
        public IActionResult Information(int? id)
        {
            if (id == null)
                return NotFound();
            User user = _db.Users.Find(id);
            if (user == null)
                return NotFound();
            return View(user);
        }


        //This method will return the view for the AccessDenied page
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
