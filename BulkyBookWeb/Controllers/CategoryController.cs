using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET action method
        public IActionResult Create()
        {
            return View();
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]//search this later in videos for class
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);//pushes information to the database 
                _db.SaveChanges();//saves all the changes
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET action method
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryToDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST action method
        [HttpPost]
        [ValidateAntiForgeryToken]//search this later in videos for class
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);//update object information on the database 
                _db.SaveChanges();//saves all the changes
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get action method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryToDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST action method
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]//search this later in videos for class
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);//update object information on the database 
            _db.SaveChanges();//saves all the changes
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}




