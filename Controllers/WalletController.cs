using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            ViewBag.WalletTypeId = new SelectList(_db.WalletTypes, "WalletTypeId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Wallet wallet)
        {
            if(ModelState.IsValid)
            {
                _db.Wallets.Add(wallet);
                _db.SaveChanges();
                TempData["success"] = "Wallet added successfully";
                return RedirectToAction("List");
            }
            TempData["error"] = "Error adding wallet";          
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            
            ViewBag.WalletTypeId = new SelectList(_db.WalletTypes, "WalletTypeId", "Name", wallet.WalletTypeId);
            return View(wallet);
        }

        [HttpPost]
        public IActionResult Edit(Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                //var editWallet = _db.Wallets.Find(wallet.WalletId);
                //editWallet.Name = wallet.Name;
                //editWallet.Balance = wallet.Balance;
                //editWallet.WalletTypeId = wallet.WalletTypeId;
                //editWallet.CreatedAt = wallet.CreatedAt;
                _db.Wallets.Update(wallet);
                _db.SaveChanges();
                TempData["success"] = "Wallet updated successfully";
                return RedirectToAction("List");
            }

            TempData["error"] = "Something wrong with the edit!";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            ViewBag.WalletTypeId = new SelectList(_db.WalletTypes, "WalletTypeId", "Name", wallet.WalletTypeId);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Wallet> walletList = _db.Wallets.Include(wlt => wlt.WalletType)
                .Include(u => u.User).Where(us=>Convert.ToString(us.UserId) == userId)
                .ToList();
            return View(walletList);
        }

        //This is for Information  of wallet
        [HttpGet]
        public IActionResult Information(int? id)
        {
            if (id == null)
                return NotFound();
            Wallet wallet = _db.Wallets.Include(us=>us.User).Include(wlt=>wlt.WalletType).FirstOrDefault(wl=>wl.WalletId == id);
            if (wallet == null)
                return NotFound();
            return View(wallet);
        }
    }
}
