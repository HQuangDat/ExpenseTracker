using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
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
                return RedirectToAction("List");
            }
            return View(user);
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
    }
}
