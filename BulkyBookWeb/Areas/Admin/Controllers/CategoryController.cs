using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
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
            _unitOfWork.Category.Add(obj);//pushes information to the database 
            _unitOfWork.Save();//saves all the changes
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
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
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
            _unitOfWork.Category.Update(obj);//update object information on the database 
            _unitOfWork.Save();//saves all the changes
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
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    //POST action method
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]//search this later in videos for class
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(obj);//update object information on the database 
        _unitOfWork.Save();//saves all the changes
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}




