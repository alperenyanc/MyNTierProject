using MyNTierProject.UI.Areas.Admin.Models.DTO;
using MyTierProject.Model.Option;
using MyTierProject.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNTierProject.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // we must use to category service so..

        CategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category data)
        {
            // no image yet. 
            _categoryService.Add(data);// add metot is context save and some add process.
            return Redirect("/Admin/Category/List"); // after the add go to catergory list!
        }
        // where is  list
        public ActionResult List()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }
        // update - delete process
        public ActionResult Update(Guid id)
        {// update metod using guid id -- and find elamnt by using getbyid metot.
            // this proccess -- database T go to model-- we needd CategoryDTO..
            Category cat = _categoryService.GetByID(id);// we found T items and items goes to cat. 
            CategoryDTO model = new CategoryDTO();
            model.Description = cat.Description;
            model.ID = cat.ID;
            model.Name = cat.Name;
            return View(model); // view -- category list!.

        }
        [HttpPost]
        public ActionResult Update(CategoryDTO data)
        {
            Category cat = _categoryService.GetByID(data.ID);
            cat.Name = data.Name;
            cat.Description = data.Description;
            _categoryService.Update(cat);
            return Redirect("/Admin/Category/List");

        }
        public ActionResult Delete(Guid id)
        {
            _categoryService.Remove(id);
            return Redirect("/Admin/Category/List");
        }

        // lets go to view!.

       
    }
}