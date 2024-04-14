using BikeSearchingSite.AppDBContext;
using BikeSearchingSite.Models;
using BikeSearchingSite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using cloudscribe.Pagination.Models;


namespace BikeSearchingSite.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class BikeController : Controller
    {
        private readonly BikeSearchDbContext _db;
        public static IWebHostEnvironment _environment;


        [BindProperty]
        public BikeViewModel BikeVM { get; set; }

        public BikeController(BikeSearchDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()

            };

        }
        [AllowAnonymous]
        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 2)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.PriceSortParam = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var Bikes = from b in _db.Bikes.Include(m => m.Make).Include(m => m.Model) select b;

            var BikeCount = Bikes.Count();
            if (!string.IsNullOrEmpty(searchString))
            {
                Bikes = Bikes.Where(b => b.Make.Name.Contains(searchString));
                BikeCount = Bikes.Count();
            }

            switch (sortOrder)
            {
                case "price_desc":
                    Bikes = Bikes.OrderByDescending(b => b.Price);
                    break;
                default:
                    Bikes = Bikes.OrderBy(b => b.Price);
                    break;
            }

            Bikes = Bikes.Skip(ExcludeRecords).Take(pageSize);

            var Result = new PagedResult<Bike>
            {
                Data = Bikes.AsNoTracking().ToList(),
                TotalItems = BikeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(Result);
        }

        public IActionResult Create()
        {
            return View(BikeVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();
                return View(BikeVM);
            }
            _db.Bikes.Add(BikeVM.Bike);
            UploadImage();
            _db.SaveChanges();            

            return RedirectToAction("Index");
        }
        private void UploadImage()
        {           
            var BikeID = BikeVM.Bike.Id;

            var files = HttpContext.Request.Form.Files;

            string wwrootPath = _environment.WebRootPath;

            var SaveBike = _db.Bikes.Find(BikeID);

            if (files.Count != 0)
            {
                var ImagePath = @"images\bike\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + BikeID + Extension;
                var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePath);

                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                SaveBike.ImagePath = RelativeImagePath;               
            }
        }

        public IActionResult Edit(int id)
        {
            BikeVM.Bike = _db.Bikes.SingleOrDefault(m => m.Id == id);
            BikeVM.Models = _db.Models.Where(m => m.MakeId == BikeVM.Bike.MakeID);

            if (BikeVM.Bike == null)
            {
                return NotFound();
            }
            return View(BikeVM);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();
                return View(BikeVM);
            }
            _db.Bikes.Update(BikeVM.Bike);
            UploadImage();
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Bike bike = _db.Bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }
            _db.Bikes.Remove(bike);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult View(int id)
        {
            BikeVM.Bike = _db.Bikes.SingleOrDefault(m => m.Id == id);          

            if (BikeVM.Bike == null)
            {
                return NotFound();
            }
            return View(BikeVM);
        }
    }
}
