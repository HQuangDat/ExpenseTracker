﻿using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpenseController(ApplicationDbContext db)
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

        [HttpGet]
        public IActionResult List()
        {
            List<Expense> expenseList = _db.Expenses.ToList();
            return View(expenseList);
        }

        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }
    }
}
