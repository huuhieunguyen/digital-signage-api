using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CMS.Services
{
    public interface IStorageService
    {
        // Task<string> UploadFileAsync(IFormFile file);
        Task<(string Url, int? Duration, string Dimensions)> UploadFileAsync(IFormFile file);

        // Task<string> UploadThumbnailAsync(IFormFile file);
    }

    public class CloudinaryStorageService : IStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        // public async Task<string> UploadFileAsync(IFormFile file)
        // {
        //     if (_cloudinary == null)
        //     {
        //         throw new InvalidOperationException("Cloudinary instance is not configured properly.");
        //     }

        //     using (var stream = file.OpenReadStream())
        //     {
        //         var uploadParams = new RawUploadParams
        //         {
        //             File = new FileDescription(file.FileName, stream)
        //         };

        //         var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        //         return uploadResult.SecureUrl.ToString();
        //     }
        // }

        public async Task<(string Url, int? Duration, string Dimensions)> UploadFileAsync(IFormFile file)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            UploadResult uploadResult;

            if (file.ContentType.Contains("image"))
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
                var dimensions = $"{uploadResult.MetadataFields["width"]}x{uploadResult.MetadataFields["height"]}";
                return (uploadResult.Url.ToString(), 10, dimensions);
            }
            else if (file.ContentType.Contains("video"))
            {
                var videoUploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
                var videoUploadResult = await _cloudinary.UploadLargeAsync(videoUploadParams);
                var duration = Convert.ToInt32(videoUploadResult.Duration);
                var dimensions = $"{videoUploadResult.Width}x{videoUploadResult.Height}";
                return (videoUploadResult.Url.ToString(), duration, dimensions);
            }
            else
            {
                throw new NotSupportedException("Unsupported media type");
            }
        }
    }
}

