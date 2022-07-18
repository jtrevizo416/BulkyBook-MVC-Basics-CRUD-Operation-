using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }

    //GET action method
    public IActionResult Create()
    {
        return View();
    }

    //POST action method
    [HttpPost]
    [ValidateAntiForgeryToken]//search this later in videos for class
    public IActionResult Create(CoverType obj)
    {
        //if (obj.Name == obj.Id.ToString())
        //{
        //    ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
        //}
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);//pushes information to the database 
            _unitOfWork.Save();//saves all the changes
            TempData["success"] = "Cover Type created successfully";
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
        var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (coverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(coverTypeFromDbFirst);
    }

    //POST action method
    [HttpPost]
    [ValidateAntiForgeryToken]//search this later in videos for class
    public IActionResult Edit(CoverType obj)
    {
        //if (obj.Name == obj.DisplayOrder.ToString())
        //{
        //    ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
        //}
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);//update object information on the database 
            _unitOfWork.Save();//saves all the changes
            TempData["success"] = "Cover Type edited successfully";
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
        var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var categoryToDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (coverTypeFromDb == null)
        {
            return NotFound();
        }
        return View(coverTypeFromDb);
    }

    //POST action method
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]//search this later in videos for class
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.CoverType.Remove(obj);//update object information on the database 
        _unitOfWork.Save();//saves all the changes
        TempData["success"] = "Cover Type deleted successfully";
        return RedirectToAction("Index");
    }
}




