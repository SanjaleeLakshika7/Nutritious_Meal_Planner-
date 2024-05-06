using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Common;
using Catalog.Models;
using Catalog.Data;
using Content.Models;
using Content.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Account.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MainWeb.Areas.Catelog.Controllers
{
    [Area("Catelog")]
    public class ItemController : Controller
    {
        #region Construction

        private readonly IItemData rep;
        private readonly IImageData repImg;
        private readonly IImageWork repWork;
        private readonly IWebHostEnvironment environment;
        private readonly ISystemData repSys;
        private readonly ISettingsData settingrep;

        public ItemController(IItemData rep, IImageData repImg, IImageWork repWork, IWebHostEnvironment environment, ISystemData repSys , ISettingsData settingrep)
        {
            this.rep = rep;
            this.repImg = repImg;
            this.repWork = repWork;
            this.environment = environment;
            this.repSys = repSys;
            this.settingrep = settingrep;
        }

        #endregion


        public async Task<IActionResult> Index(string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string ActiveStatus = "")
        {
            Auth.CheckUser();

            var obj = new ItemSearchView();
            obj.KeyW = KeyW;
            obj.CategoryMainID = CategoryMainID;
            obj.CategorySubID = CategorySubID;
            obj.BrandID = BrandID;
            obj.StockAvailable = StockAvailable;
            obj.ActiveStatus = ActiveStatus;
            obj.RecordCount = await rep.GetCount
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    KeyW: KeyW.ToBlank(),
                                    CategoryMainID: CategoryMainID.ToBlank(),
                                    CategorySubID: CategorySubID.ToBlank(),
                                    BrandID: BrandID.ToBlank(),
                                    StockAvailable: StockAvailable.ToBlank(),
                                    ActiveStatus: ActiveStatus.ToBlank()
                                );
            obj.PaginationList = AppData.GetPaginationList(obj.RecordCount, obj.PageSize);

            return View(obj);
        }

        public async Task<IActionResult> NoImage(string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string ActiveStatus = "")
        {
            Auth.CheckUser();

            var obj = new ItemSearchView();
            obj.KeyW = KeyW;
            obj.CategoryMainID = CategoryMainID;
            obj.CategorySubID = CategorySubID;
            obj.BrandID = BrandID;
            obj.StockAvailable = StockAvailable;
            obj.ActiveStatus = ActiveStatus;
            
            
            var itemList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CategoryMainID: CategoryMainID.ToBlank(),
                                CategorySubID: CategorySubID.ToBlank(),
                                BrandID: BrandID.ToBlank(),
                                StockAvailable: StockAvailable.ToBlank(),
                                ActiveStatus: ActiveStatus.ToBlank()
                            );

            foreach(var itm in itemList)
            {
                var ParerntPath = Directory.GetParent(environment.ContentRootPath).FullName;
                string pathOLD = ParerntPath + "\\" + "Uploads" + "\\" + itm.ItemID + ".jpg";

                if (!System.IO.File.Exists(pathOLD))
                {
                    obj.DataList.Add(itm);
                }



            }
           
            obj.RecordCount = obj.DataList.Count;
        


            return View(obj);
        }

        public async Task<IActionResult> Add()
        {
            Auth.CheckUser();

            var obj = new ItemEditView();

            var itm = new Item();
            itm.ItemTypeID = "STK";
            itm.CategorySubName = "Select category";
            itm.ActiveStatus = "A";
            itm.WholesalePrice = 0;

            var set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
            itm.cat_ImageSizeWidth = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
            itm.cat_ImageSizeHeight = set.SettingValue;

            itm.ItemCode = await repSys.GetNextKey("AutoItemCode", "");

            obj.Basic = itm;

            var UserID = Auth.GetUserID();
            repWork.ClearTempImages(UserID);

            

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemEditView obj)
        {
            Auth.CheckUser();
            try
            {
                obj.Basic.ItemID = "";
                obj.Basic.ItemID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Basic,
                                       LogUserID: ""
                                    );
                var UserID = Auth.GetUserID();
                var AdImageCreated = false;
                 var CreatedThumbnailImage = false;

                var TempImgList = await repImg.Temp_GetList
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       ID: UserID
                                    );

                foreach (var img in TempImgList)
                {
                    var newImg = new ImageUpload();
                    newImg.ImageID = "";
                    newImg.RefID = obj.Basic.ItemID;

                    var NewImgID = await repImg.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: newImg,
                                       LogUserID: ""
                                    );

                    IHttpContextAccessor context = new HttpContextAccessor();
                    var request = context.HttpContext.Request;

                    var oldFilename = repWork.GetUploadPath(img.ImageID + ".jpg");
                    var newFilename = repWork.GetUploadPath(NewImgID + ".jpg");

                    System.IO.File.Move(oldFilename, newFilename);

                    // save prof picture

                    if (AdImageCreated == false)
                    {
                        var cat_ImageSizeHeight = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
                        var cat_ImageSizeWidth = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
                       
                        if (obj.Basic.FileUpload != null)
                        {
                           
                            var filePath = repWork.GetUploadPath(img.ImageID + ".jpg");
                          
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                           
                            }

                            if (System.IO.File.Exists(newFilename))
                            {
                                System.IO.File.Delete(newFilename);

                            }

                            var filenameNew = obj.Basic.ItemID + ".jpg";
                            var filePathNew = repWork.GetUploadPath(filenameNew);
                            obj.Basic.FileUpload.CopyTo(new FileStream(filePathNew, FileMode.Create));
                            obj.Basic.FileUpload.CopyTo(new FileStream(newFilename, FileMode.Create));
                            CreatedThumbnailImage = true;
                        }
                        else {
                            repWork.PostImgCreate(newFilename, obj.Basic.ItemID, cat_ImageSizeHeight.SettingValue, cat_ImageSizeWidth.SettingValue);
                        }
                      
                        AdImageCreated = true;
                    }

                    
                }

                if (obj.Basic.FileUpload != null && CreatedThumbnailImage == false)
                {
                   
                    ImageUpload img = new ImageUpload();
                    img.ImageID = "";
                    img.RefID = UserID;
                    var ImageID = await repImg.Temp_AddEdit
                                   (
                                      APIKey: AppData.GetAPIKey(),
                                      obj: img,
                                      LogUserID: ""
                                   );

                    var newImg = new ImageUpload();
                    newImg.ImageID = "";
                    newImg.RefID = obj.Basic.ItemID;
                    var NewImgID = await repImg.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: newImg,
                                       LogUserID: ""
                                    );

                    var filenameNew = obj.Basic.ItemID + ".jpg";
                    var filePathNew = repWork.GetUploadPath(filenameNew);
                    obj.Basic.FileUpload.CopyTo(new FileStream(filePathNew, FileMode.Create));

                    var newFilename = repWork.GetUploadPath(NewImgID + ".jpg");
                    obj.Basic.FileUpload.CopyTo(new FileStream(newFilename, FileMode.Create));

                 
                }
                repWork.ClearTempImages(UserID);

                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Auth.CheckUser();
            var obj = new ItemEditView();

            obj.Basic = await rep.Get(AppData.GetAPIKey(), id);
            obj.Basic.CategorySubName = obj.Basic.CategoryDisplay;
            obj.Basic.ImageList = await repImg.GetList(AppData.GetAPIKey(), id);

            var set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
            obj.Basic.cat_ImageSizeWidth = set.SettingValue;

            set = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
            obj.Basic.cat_ImageSizeHeight = set.SettingValue;

            var UserID = Auth.GetUserID();
            repWork.ClearTempImages(UserID);

            try
            {
                obj.ReturnURL = Request.Headers["Referer"].ToString();
            }
            catch { }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemEditView obj)
        {
            Auth.CheckUser();

            try
            {
                
                obj.Basic.ItemID = await rep.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: obj.Basic,
                                       LogUserID: ""
                                    );

                var UserID = Auth.GetUserID();
               

                var AdImageCreated = false; 
                var CreatedThumbnailImage = false;

                var TempImgList = await repImg.Temp_GetList
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       ID: UserID
                                    );

                foreach (var img in TempImgList)
                {
                    var newImg = new ImageUpload();
                    newImg.ImageID = "";
                    newImg.RefID = obj.Basic.ItemID;

                    var NewImgID = await repImg.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: newImg,
                                       LogUserID: ""
                                    );

                    IHttpContextAccessor context = new HttpContextAccessor();
                    var request = context.HttpContext.Request;

                    var oldFilename = repWork.GetUploadPath(img.ImageID + ".jpg");
                    var newFilename = repWork.GetUploadPath(NewImgID + ".jpg");

                    System.IO.File.Move(oldFilename, newFilename);
                }

                var ImgList = await repImg.GetList
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       ID: obj.Basic.ItemID
                                    );

                foreach (var img in ImgList)
                {
                    // save prof picture
                    if (AdImageCreated == false)
                    {
                        var newFilename = repWork.GetUploadPath(img.ImageID + ".jpg");
                        var cat_ImageSizeHeight = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
                        var cat_ImageSizeWidth = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");

                        if (obj.Basic.FileUpload != null)
                        {

                            var filePath = repWork.GetUploadPath(img.ImageID + ".jpg");

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);

                            }

                            if (System.IO.File.Exists(newFilename))
                            {
                                System.IO.File.Delete(newFilename);

                            }

                            var filenameNew = obj.Basic.ItemID + ".jpg";
                            var filePathNew = repWork.GetUploadPath(filenameNew);
                            obj.Basic.FileUpload.CopyTo(new FileStream(filePathNew, FileMode.Create));
                            obj.Basic.FileUpload.CopyTo(new FileStream(newFilename, FileMode.Create));
                            CreatedThumbnailImage = true;
                        }
                        else
                        {
                            repWork.PostImgCreate(newFilename, obj.Basic.ItemID, cat_ImageSizeHeight.SettingValue, cat_ImageSizeWidth.SettingValue);
                        }

                        AdImageCreated = true;
                    }
                }



                if (obj.Basic.FileUpload != null && CreatedThumbnailImage == false)
                {

                    ImageUpload img = new ImageUpload();
                    img.ImageID = "";
                    img.RefID = UserID;
                    var ImageID = await repImg.Temp_AddEdit
                                   (
                                      APIKey: AppData.GetAPIKey(),
                                      obj: img,
                                      LogUserID: ""
                                   );

                    var newImg = new ImageUpload();
                    newImg.ImageID = "";
                    newImg.RefID = obj.Basic.ItemID;
                    var NewImgID = await repImg.AddEdit
                                    (
                                       APIKey: AppData.GetAPIKey(),
                                       obj: newImg,
                                       LogUserID: ""
                                    );

                    var filenameNew = obj.Basic.ItemID + ".jpg";
                    var filePathNew = repWork.GetUploadPath(filenameNew);
                    obj.Basic.FileUpload.CopyTo(new FileStream(filePathNew, FileMode.Create));

                    var newFilename = repWork.GetUploadPath(NewImgID + ".jpg");
                    obj.Basic.FileUpload.CopyTo(new FileStream(newFilename, FileMode.Create));


                }
                return Redirect(obj.ReturnURL);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Unable to save record. " + ex.Message;
            }

            return View(obj);
        }



        // Partial

        public async Task<ActionResult> _MainList(string KeyW = "", string CategoryMainID = "", string CategorySubID = "", string BrandID = "", string StockAvailable = "", string ActiveStatus = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                CategoryMainID: CategoryMainID.ToBlank(),
                                CategorySubID: CategorySubID.ToBlank(),
                                BrandID: BrandID.ToBlank(),
                                StockAvailable: StockAvailable.ToBlank(),
                                ActiveStatus: ActiveStatus.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }

        public async Task<ActionResult> _ItemTick(string KeyW = "", int Page = 1, int PageSize = 99999)
        {
            Auth.CheckUserPartial();

            var objList = await rep.GetList
                            (
                                APIKey: AppData.GetAPIKey(),
                                KeyW: KeyW.ToBlank(),
                                Page: Page,
                                PageSize: PageSize
                            );

            return View(objList);
        }


        // JSon

        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                if (id == null || id == "")
                {
                    return Json(new { success = false, responseText = "Invalid request or bad parameters" });
                }

                //var emp = (Employee)Session["Employee"];

                var RetValue = await rep.Delete
                                (
                                    APIKey: AppData.GetAPIKey(),
                                    ID: id,
                                    LogUserID: ""
                                );

                return Json(new { success = true, responseText = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                return Json(new { success = false, responseText = "Unable to delete record. " + ex.Message });
            }
        }


    }
}
