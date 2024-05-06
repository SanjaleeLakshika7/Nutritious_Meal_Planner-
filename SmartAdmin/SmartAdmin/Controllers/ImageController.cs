using Catalog.Data;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Account.Models;

namespace SmartAdmin.Controllers
{
    public class ImageController : Controller
    {
        #region Construction

        private readonly IImageData rep;
        private IWebHostEnvironment environment;
        private readonly IImageWork repWork;

        public ImageController(IImageData rep, IWebHostEnvironment environment, IImageWork repWork)
        {
            this.rep = rep;
            this.environment = environment;
            this.repWork = repWork;
        }

        #endregion

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> AddImage(string base64image, string RefID)
        {
            try
            {
                if (string.IsNullOrEmpty(base64image))
                    return null;

                ImageUpload img = new ImageUpload();
                img.ImageID = "";
                img.RefID = RefID;

                img.ImageID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: img,
                                       LogUserID: ""
                                    );

                var t = base64image.Substring(22);

                byte[] bytes = Convert.FromBase64String(t);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                var filename = img.ImageID + ".jpg";
                var imgUrl = "/Uploads/" + filename;
                image.Save(repWork.GetUploadPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);

                return Json(new { imageID = img.ImageID, imgUrl = imgUrl, errorMessage = "" });
            }
            catch (Exception ex)
            {
                return Json(new { imageID = "", imgUrl = "", errorMessage = ex.Message });
            }


        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> SaveImage(string base64image)
        {
            try
            {
                if (string.IsNullOrEmpty(base64image))
                    return null;

                IHttpContextAccessor _context = new HttpContextAccessor();

                if (_context.HttpContext.Session.GetObject<User>("User") == null)
                    return null;

                User usr = _context.HttpContext.Session.GetObject<User>("User");

                ImageUpload img = new ImageUpload();
                img.ImageID = "";
                img.RefID = usr.UserID;

                img.ImageID = await rep.Temp_AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: img,
                                       LogUserID: ""
                                    );

                var t = base64image.Substring(22);

                byte[] bytes = Convert.FromBase64String(t);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                var filename = img.ImageID + ".jpg";
                var imgUrl = "/Uploads/" + filename;
                image.Save(repWork.GetUploadPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);

                return Json(new { imageID = img.ImageID, imgUrl = imgUrl, errorMessage = "" });
            }
            catch (Exception ex)
            {
                return Json(new { imageID = "", imgUrl = "", errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteImage(string ImageID)
        {
            try
            {
                if (string.IsNullOrEmpty(ImageID))
                    return null;

                await rep.Delete
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    ID: ImageID,
                                    LogUserID: ""
                                );

                var fullPath = Path.Combine(AppData.GetUplaodPath(), ImageID + ".jpg");
                System.IO.File.Delete(fullPath);

                return Json(new { imageID = ImageID, imgUrl = "", errorMessage = "" });
            }
            catch (Exception ex)
            {
                return Json(new { imageID = ImageID, imgUrl = "", errorMessage = ex.Message });
            }
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> UpdateAddImage(string base64image, string RefID, string ImageID)
        {
            try
            {
                if (string.IsNullOrEmpty(base64image))
                    return null;

                ImageUpload img = new ImageUpload();
                img.ImageID = ImageID;
                img.RefID = RefID;



                var t = base64image.Substring(22);

                byte[] bytes = Convert.FromBase64String(t);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                var fullPathUpdate = Path.Combine(repWork.SetUploadPath(), ImageID + ".jpg");
                System.IO.File.Delete(fullPathUpdate);

                var filename = img.ImageID + ".jpg";
                var imgUrl = "/Uploads/" + filename;
                image.Save(repWork.GetUploadPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);

                return Json(new { imageID = img.ImageID, imgUrl = imgUrl, errorMessage = "" });
            }
            catch (Exception ex)
            {
                return Json(new { imageID = "", imgUrl = "", errorMessage = ex.Message });
            }


        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<JsonResult> UpdateSaveImage(string base64image, string ImageID)
        {
            try
            {
                if (string.IsNullOrEmpty(base64image))
                    return null;

                IHttpContextAccessor _context = new HttpContextAccessor();

                if (_context.HttpContext.Session.GetObject<User>("User") == null)
                    return null;

                User usr = _context.HttpContext.Session.GetObject<User>("User");

                ImageUpload img = new ImageUpload();
                img.ImageID = ImageID;
                img.RefID = usr.UserID;


                var t = base64image.Substring(22);

                byte[] bytes = Convert.FromBase64String(t);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                var fullPathUpdate = Path.Combine(repWork.SetUploadPath(), ImageID + ".jpg");
                System.IO.File.Delete(fullPathUpdate);
                var filename = img.ImageID + ".jpg";
                var imgUrl = "/Uploads/" + filename;
                image.Save(repWork.GetUploadPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);

                return Json(new { imageID = img.ImageID, imgUrl = imgUrl, errorMessage = "" });
            }
            catch (Exception ex)
            {
                return Json(new { imageID = "", imgUrl = "", errorMessage = ex.Message });
            }
        }


    }
}
