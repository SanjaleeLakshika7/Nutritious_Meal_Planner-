public interface IImageWork
{
    void ClearTempImages(string RefID);
    string GetUploadPath(string filename);
    void PostImgCreate(string filePath, string NewID, string cat_ImageSizeHeight, string cat_ImageSizeWidth);
    void RecreatePostImg(string RefID);

    string SetUploadPath();
}