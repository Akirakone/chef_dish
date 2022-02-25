using Microsoft.EntityFrameworkCore;
using chef_dish.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace chef_dish.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        // here we can "inject" our context service into the constructor
        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.Include(d => d.Preparer).OrderByDescending(d => d.CreatedAt).ToList();
            return View("index");
        }

        [HttpGet("AddDish")]
        public IActionResult AddDish()
        {
            List<Chef> allchefs = _context.Chefs.ToList();
            ViewBag.allchefs = allchefs;
            return View("AddDish");
        }

        [HttpPost("newDish")]
        public IActionResult NewDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            List<Chef> allchefs = _context.Chefs.ToList();
            ViewBag.allchefs = allchefs;
            return View("AddDish");
            }

        [HttpGet("AddChef")]
        public IActionResult AddChef()
        {
            List<Chef> allchefs = _context.Chefs.ToList();
            ViewBag.allchefs = allchefs;
            return View("AddChef");
        }

        [HttpPost("newChef")]
        public IActionResult NewChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                if((DateTime.Now.Year - newChef.DOB.Year) < 18 )
                {
                    ModelState.AddModelError("DOB", "Must Be 18 or Older!");
                    return View("AddChef");
                }
                _context.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return View("AddChef");
        }

        [HttpGet("Allchefs")]
        public IActionResult Allchefs()
        {
            List<Chef> allchefs = _context.Chefs.Include(e=>e.AllDishes).ToList();
            ViewBag.allchefs = allchefs;
            return View("Allchefs");
        }
    }
}