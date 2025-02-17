using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class WalletController : Controller
    {
        private readonly ApplicationDbContext _db;
        public WalletController(ApplicationDbContext db)
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
            List<Wallet> walletList = _db.Wallets.ToList();
            return View(walletList);
        }

        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }

    }
}
