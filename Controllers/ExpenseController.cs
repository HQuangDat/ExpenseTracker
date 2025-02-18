using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        //This method will return the view for the Add Expense page
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //These two methods will edit the expense in the database
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Expense expense = _db.Expenses.Find(id);
            if (expense == null)
                return NotFound();
            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(expense);
        }

        //This method will delete the expense from the database
        [HttpDelete]
        public IActionResult Delete(Expense expense)
        {
            if(ModelState.IsValid)
            {
                _db.Expenses.Remove(expense);
                _db.SaveChanges();
                return RedirectToRoute("Home", "Index");
            }
            return View();
        }

        //This method will return the view for the list of expenses
        [HttpGet]
        public IActionResult List()
        {
            List<Expense> expenseList = _db.Expenses.ToList();
            return View(expenseList);
        }

        [HttpGet]
        public IActionResult Information(int? id)
        {
            if (id == null)
                return NotFound();
            Expense expense = _db.Expenses.Find(id);
            if (expense == null)
                return NotFound();
            return View(expense);
        }
    }
}
