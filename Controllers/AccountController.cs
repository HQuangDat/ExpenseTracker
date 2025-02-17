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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpDelete]
        public IActionResult Delete()
        {
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
