using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class WalletController : Controller
    {
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
    }
}
