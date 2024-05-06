using Catalog.Data;
using Content.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ImageWork : IImageWork
{
    private readonly IImageData rep;
    private readonly IWebHostEnvironment environment;
    private readonly ISettingsData settingrep;

    public ImageWork(IImageData rep, IWebHostEnvironment environment, ISettingsData settingrep)
    {
        this.rep = rep;
        this.environment = environment;
        this.settingrep = settingrep;
    }

    public async void ClearTempImages(string RefID)
    {
        var TempImgList = await rep.Temp_GetList(AppData.GetAPIKey(), RefID);

        foreach (var img in TempImgList)
        {
            var oldFilename = GetUploadPath(img.ImageID + ".jpg");
            File.Delete(oldFilename);
        }

        await rep.Temp_Clear(AppData.GetAPIKey(), RefID, "");
    }

    

    public void PostImgCreate(string filePath, string NewID,string cat_ImageSizeHeight ,string cat_ImageSizeWidth)
    {
        
        Image imgOld = Image.FromFile(filePath);
        Image imgNew = ResizeImage(imgOld, Convert.ToInt32(cat_ImageSizeWidth), Convert.ToInt32(cat_ImageSizeHeight));

        var filename = NewID + ".jpg";
        imgNew.Save(GetUploadPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);
    }

    public static System.Drawing.Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
    {
        //a holder for the result 
        Bitmap result = new Bitmap(width, height);

        //use a graphics object to draw the resized image into the bitmap 
        using (Graphics graphics = Graphics.FromImage(result))
        {
            //set the resize quality modes to high quality 
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //draw the image into the target bitmap 
            graphics.DrawImage(image, 0, 0, result.Width, result.Height);
        }

        //return the resulting bitmap 
        return result;
    }

    public async void RecreatePostImg(string RefID)
    {
        try
        {
            var AdImageCreated = false;
            var imgList = await rep.GetList(AppData.GetAPIKey(), RefID);
            var cat_ImageSizeHeight = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeHeight");
            var cat_ImageSizeWidth = await settingrep.Get(APIKey: AppData.GetAPIKey(), SettingName: "cat_ImageSizeWidth");
            foreach (var img in imgList)
            {
                if (AdImageCreated == false)
                {
                    var newFilename = GetUploadPath(img.ImageID + ".jpg");
                    PostImgCreate(newFilename, RefID, cat_ImageSizeHeight.SettingValue, cat_ImageSizeWidth.SettingValue);
                    AdImageCreated = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetUploadPath(string filename)
    {
        var ParerntPath = Directory.GetParent(environment.ContentRootPath).FullName;
        var FullPath = ParerntPath + "\\" + AppData.GetUplaodPath() + "\\" + filename;
        return FullPath;
    }

    public string SetUploadPath()
    {
        var ParerntPath = Directory.GetParent(environment.ContentRootPath).FullName;
        var FullPath = ParerntPath + "\\" + AppData.GetUplaodPath() + "\\" ;
        return FullPath;
    }
}
