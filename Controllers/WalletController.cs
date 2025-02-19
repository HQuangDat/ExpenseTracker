using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                TempData["success"] = "Wallet type added successfully";
                return RedirectToAction("List");
            }
            return View(walletType);
        }

        //These two methods will edit the wallet in the database
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Wallet wallet = _db.Wallets
                .Include(wlt => wlt.WalletType)
                .Include(wlt => wlt.User)
                .FirstOrDefault(wl => wl.WalletId == id);
            if (wallet == null)
                return NotFound();
            ViewBag.UserId = wallet.UserId;
            ViewBag.WalletTypeId = new SelectList(_db.WalletTypes, "WalletTypeId", "Name", wallet.WalletTypeId);
            return View(wallet);
        }

        [HttpPost]
        public IActionResult Edit(Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _db.Wallets.Update(wallet);
                _db.SaveChanges();
                TempData["success"] = "Wallet updated successfully";
                return RedirectToAction("List");
            }
            return View(wallet);
        }

        //This is for edit type
        [HttpGet]
        public IActionResult EditType(int? id)
        {
            if (id == null)
                return NotFound();
            WalletType walletType = _db.WalletTypes.Find(id);
            if (walletType == null)
                return NotFound();
            return View(walletType);
        }
        [HttpPost]
        public IActionResult EditType(WalletType walletType)
        {
            if (ModelState.IsValid)
            {
                _db.WalletTypes.Update(walletType);
                _db.SaveChanges();
                TempData["success"] = "Wallet type updated successfully";
                return RedirectToAction("List");
            }
            return View(walletType);
        }

        //Delete wallet function
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Wallet wallet = _db.Wallets.Find(id);
            if(wallet == null)
            {
                TempData["error"] = "Wallet not found";
                return NotFound();
            }
            _db.Wallets.Remove(wallet);
            _db.SaveChanges();
            TempData["success"] = "Wallet deleted successfully";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Wallet> walletList = _db.Wallets.Include(wlt => wlt.WalletType)
                .Include(u => u.User)
                .ToList();
            return View(walletList);
        }

        //This is for detail information of wallet
        [HttpGet]
        public IActionResult Information(int? id)
        {
            if (id == null)
                return NotFound();
            Wallet wallet = _db.Wallets.Find(id);
            if (wallet == null)
                return NotFound();
            return View(wallet);
        }
    }
}
