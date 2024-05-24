namespace CMS.Services
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
        // Task<string> UploadThumbnailAsync(IFormFile file);
    }
}
