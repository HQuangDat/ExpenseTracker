using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWallets = _db.Wallets.Where(us =>Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.WalletId = new SelectList(userWallets, "WalletId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Expense expense)
        {
            if (ModelState.IsValid)
            {
                var exWallet = _db.Wallets.FirstOrDefault(wl => wl.WalletId == expense.WalletId);
                exWallet.Balance -= expense.Amount;
                _db.Expenses.Add(expense);
                _db.Wallets.Update(exWallet);
                _db.SaveChanges();
                TempData["success"] = "Expense added successfully";
                return RedirectToAction("List");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            TempData.Add("error", "Error adding expense");
            var userWallets = _db.Wallets.Where(us => Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.WalletId = new SelectList(userWallets, "WalletId", "Name");
            return View(expense);
        }

        //This is Earn Expense Method
        [HttpGet]
        public IActionResult Earn()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWallets = _db.Wallets.Where(us => Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.WalletId = new SelectList(userWallets, "WalletId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Earn(Expense expense)
        {
            if (ModelState.IsValid)
            {
                var exWallet = _db.Wallets.FirstOrDefault(wl => wl.WalletId == expense.WalletId);
                exWallet.Balance += expense.Amount;
                _db.Expenses.Add(expense);
                _db.Wallets.Update(exWallet);
                _db.SaveChanges();
                TempData["success"] = "Expense added successfully";
                return RedirectToAction("List");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            TempData.Add("error", "Error adding expense");
            var userWallets = _db.Wallets.Where(us => Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            ViewBag.WalletId = new SelectList(userWallets, "WalletId", "Name");
            return View(expense);
        }
        //These two methods will edit the expense in the database
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "The expense doesn't exist!";
                return RedirectToAction("List", "Expense");
            }
            Expense expense = _db.Expenses
                .Include(ctg => ctg.Category)
                .Include(wl => wl.Wallet)
                .Include(us=>us.User)
                .FirstOrDefault(exp=>exp.ExpenseId == id);
            if (expense == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWallet = _db.Wallets.Where(us => Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories,"CategoryId","Name",expense.ExpenseId);
            ViewBag.WalletId = new SelectList(userWallet, "WalletId", "Name", expense.ExpenseId);
            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                TempData["success"] = "Expense updated successfully";
                return RedirectToAction("List");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWallet = _db.Wallets.Where(us => Convert.ToString(us.UserId) == userId).ToList();
            ViewBag.UserId = userId;
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", expense.ExpenseId);
            ViewBag.WalletId = new SelectList(userWallet, "WalletId", "Name", expense.ExpenseId);
            TempData["error"] = "Error updating expense";
            return View(expense);
        }

        //This method will delete the expense from the database
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Expense expense = _db.Expenses.Find(id);
            var delWallet = _db.Wallets.FirstOrDefault(wl=>wl.WalletId == expense.WalletId);
            if (id == null)
            {
                TempData["error"] = "Expense not found";
                return NotFound();
            }

            if(expense.Type == "Earn")
            {
                delWallet.Balance -= expense.Amount;
                _db.Update(delWallet);
            }
            else if(expense.Type == "Spend")
            {
                delWallet.Balance += expense.Amount;
                _db.Update(delWallet);
            }
            _db.Expenses.Remove(expense);
            _db.SaveChanges();
            TempData["success"] = "Expense deleted successfully";
            return RedirectToAction("List");
        }

        //This method will return the view for the list of expenses
        [HttpGet]
        public IActionResult List()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Expense> expenseList = _db.Expenses
                    .Include(ct => ct.Category)
                    .Include(wl => wl.Wallet)
                    .Include(us => us.User).Where(us => Convert.ToString(us.UserId) == userId)
                    .ToList();
                return View(expenseList);
            }
        }

        [HttpGet]
        public IActionResult Information(int? id)
        {
            if (id == null)
                return NotFound();
            Expense expense = _db.Expenses.Include(us=>us.User)
                .Include(wl=>wl.Wallet)
                .Include(ct=>ct.Category)
                .FirstOrDefault(ex=>ex.ExpenseId == id);
            if (expense == null)
                return NotFound();
            return View(expense);
        }

        //Statistics method
        [HttpGet]
        public IActionResult Statistics()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                TempData["error"] = "User not found";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var userExpenses = _db.Expenses
                    .Include(ct => ct.Category)
                    .Include(wl => wl.Wallet)
                    .Include(us => us.User)
                    .Where(us => Convert.ToString(us.UserId) == userId)
                    .GroupBy(ct => ct.Category.Name)
                    .Select(g => new
                    {
                        Category = g.Key,
                        TotalAmount = g.Sum(e => e.Amount) 
                    })
                    .ToList();
                return View(userExpenses);
            }
        }
    }
}
