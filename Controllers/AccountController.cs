using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExpenseTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        //This method will return the view for the add new User page
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
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(user);
        }

        //This method will return the view for the Edit Account page
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        //This method will delete the user from the database
        [HttpDelete]
        public IActionResult Delete(User user)
        {
            if(ModelState.IsValid)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToRoute("Home","Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult List()
        {
            List<User> userList = _db.Users.ToList();
            return View(userList);
        }

        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }
    }
}
