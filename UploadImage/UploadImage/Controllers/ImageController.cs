using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadImage.Models;

namespace UploadImage.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            List<Image> imageModel = new List<Image>();
            using (DbModels db = new DbModels())
            {
                imageModel = db.Images.ToList();
            }
            return View(imageModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Image imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);
            using (DbModels db = new DbModels())
            {
                db.Images.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Image imageModel = new Image();
            using (DbModels db = new DbModels())
            {
                imageModel = db.Images.Where(x => x.ImageID == id).FirstOrDefault();
               
            }
            return View(imageModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            DbModels db = new DbModels();
            var getdata = db.Images.Find(id);
            //string path = getdata.ImagePath;
            db.Entry(getdata).State = EntityState.Deleted;
            db.SaveChanges();
            return Json(getdata, JsonRequestBehavior.AllowGet);
        }
    }
}