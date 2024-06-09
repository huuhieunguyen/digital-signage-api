using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CMS.Services
{
    public interface IStorageService
    {
        // Task<string> UploadFileAsync(IFormFile file);
        Task<(string Url, int? Duration, int? Width, int? Height, string Dimensions)> UploadFileAsync(IFormFile file);

        // Task<string> UploadThumbnailAsync(IFormFile file);
    }

    public class CloudinaryStorageService : IStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<(string Url, int? Duration, int? Width, int? Height, string Dimensions)> UploadFileAsync(IFormFile file)
        {
            if (_cloudinary == null)
            {
                throw new InvalidOperationException("Cloudinary instance is not configured properly.");
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            };

            UploadResult uploadResult;

            if (file.ContentType.Contains("image"))
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult == null || uploadResult.JsonObj == null)
                {
                    throw new ArgumentNullException(nameof(uploadResult), "uploadResult or its JsonObj property is null");
                }
                var width = (int?)uploadResult.JsonObj["width"];
                var height = (int?)uploadResult.JsonObj["height"];
                var dimensions = $"{width}x{height}";

                return (uploadResult.Url.ToString(), 10, width, height, dimensions);
            }
            else if (file.ContentType.Contains("video"))
            {
                var videoUploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
                var videoUploadResult = await _cloudinary.UploadLargeAsync(videoUploadParams);
                var duration = Convert.ToInt32(videoUploadResult.Duration);
                var width = (int?)videoUploadResult.JsonObj["width"];
                var height = (int?)videoUploadResult.JsonObj["height"];
                var dimensions = $"{width}x{height}";

                return (videoUploadResult.Url.ToString(), duration, width, height, dimensions);
            }
            else
            {
                throw new NotSupportedException("Unsupported media type");
            }
        }

        // Download file
    }
}

