public interface IImageWork
{
    void ClearTempImages(string RefID);
    string GetUploadPath(string filename);
    void PostImgCreate(string filePath, string NewID);
    void RecreatePostImg(string RefID);
}