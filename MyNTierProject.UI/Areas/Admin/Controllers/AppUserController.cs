using MyNTierProject.UI.Areas.Admin.Models.DTO;
using MyTierProject.Model.Option;
using MyTierProject.Service.Option;
using MyTierProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNTierProject.UI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {
        AppUserService _appUserService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AppUser data,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            data.UserImage = UploadedImagePaths[0];
            if (data.UserImage=="0"|| data.UserImage=="1"||data.UserImage=="2")

            {
                data.UserImage = ImageUploader.DefaultProfileImagePath;
                data.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                data.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;

            }
            else
            {
                data.XSmallUserImage = UploadedImagePaths[1];
                data.CruptedUserImage = UploadedImagePaths[2];
            }
            data.Status = MyTierProject.Core.Enum.Status.Active;
            _appUserService.Add(data);//context and save process!
            return Redirect("/Admin/AppUser/List");// after add go to List
        }
        public  ActionResult List()
        {
            // list of  active apusers 
            List<AppUser> model = _appUserService.GetActive();
            return View(model);// return (admin/appuser/List page)!

        }
        public ActionResult Update(Guid id)
        {
            AppUser user = _appUserService.GetByID(id);// getbyıd find ID and set T
                                                       /*
                                                         public T GetByID(Guid id)
                                                            {
                                                                 return context.Set<T>().Find(id);
                                                            }
                                                        */
                                                       // wee need model because context to model.
            AppUserDTO model = new AppUserDTO();
            model.ID = user.ID;// added user find to id to T goes to user. and user to model.
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.Address = user.Address;
            model.PhoneNumber = user.PhoneNumber;
            model.UserImage = user.UserImage;
            model.XSmallUserImage = user.XSmallUserImage;
            model.CruptedUserImage = user.CruptedUserImage;
            model.Role = user.Role;

            // need return!
            return View(model);// model= appuser/list page!
        }
        [HttpPost]
        public ActionResult Update(AppUserDTO data,HttpPostedFileBase Image)
        {
            // model to update(in service) update= 
            /*public void Update(T item)
            {
                T updated = GetByID(item.ID);
                DbEntityEntry entry = context.Entry(updated); 
                entry.CurrentValues.SetValues(item);
                Save();
            }*/
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.UserImage = UploadedImagePaths[0];


            AppUser update = _appUserService.GetByID(data.ID);
            if (data.UserImage == "0" || data.UserImage == "1" || data.UserImage == "2")
            {

                if (update.UserImage == null || update.UserImage == ImageUploader.DefaultProfileImagePath)
                {
                    update.UserImage = ImageUploader.DefaultProfileImagePath;
                    update.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                    update.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    update.UserImage = update.UserImage;
                    update.XSmallUserImage = update.XSmallUserImage;
                    update.CruptedUserImage = update.CruptedUserImage;
                }

            }
            else
            {
                update.UserImage = UploadedImagePaths[0];
                update.XSmallUserImage = UploadedImagePaths[1];
                update.CruptedUserImage = UploadedImagePaths[2];
            }

            // T inside this property 
            update.FirstName = data.FirstName;
            update.LastName = data.LastName;
            update.UserName = data.UserName;
            update.Email = data.Email;
            update.Address = data.Address;
            update.PhoneNumber = data.PhoneNumber;
            update.Birthdate = data.Birthdate;
            update.Role = data.Role;
            _appUserService.Update(update);

            return Redirect("/Admin/AppUser/List");// redicret to list! because just update.
        }
        // delete process
        public ActionResult Delete(Guid id)
        {
            _appUserService.Remove(id);// Remoce func-
            return Redirect("/Admin/AppUser/List");// just delete process.

            /*
             public void Remove(Guid id)
              {
                T item = GetByID(id);
                 item.Status = Core.Enum.Status.Deleted;
                Update(item);  GetByID 
              }
              get by ıd proceess..
             
               /*
                  public T GetByID(Guid id)
                   {
                     return context.Set<T>().Find(id);
                   }
                   */
                   // let go to view -- add view list view and update view!
        }




       
    }
}