using BikeSearchingSite.AppDBContext;
using BikeSearchingSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeSearchingSite.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class MakeController : Controller
    {
        private readonly BikeSearchDbContext _db;
        public MakeController(BikeSearchDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Makes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                _db.Add(make);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(make);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var make = _db.Makes.Find(Id);
            if (make == null)
            {
                return NotFound();
            }
            _db.Makes.Remove(make);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var make = _db.Makes.Find(Id);
            if (make == null)
            {
                return NotFound();
            }
            return View(make);
        }

        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _db.Update(make);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(make);
        }
    }
}
