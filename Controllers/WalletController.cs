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

        //This method will return the view for the add new Wallet page
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //And this method will return the view for the add new  Wallet type page
        [HttpGet]
        public IActionResult AddType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddType(WalletType walletType)
        {
            if (ModelState.IsValid)
            {
                _db.WalletTypes.Add(walletType);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(walletType);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(Wallet wallet)
        {
            if(ModelState.IsValid)
            {
                _db.Wallets.Remove(wallet);
                _db.SaveChanges();
                return RedirectToRoute("Home", "Index");
            }
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
