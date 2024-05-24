using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CMS.Services;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CMS.Services
{
    public class CloudinaryStorageService : IStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (_cloudinary == null)
            {
                throw new InvalidOperationException("Cloudinary instance is not configured properly.");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
        // public async Task<string> UploadThumbnailAsync(IFormFile file)
        // {
        //     using (var thumbnailStream = new MemoryStream())
        //     {
        //         if (file.ContentType.StartsWith("image"))
        //         {
        //             using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
        //             {
        //                 image.Mutate(x => x.Resize(new ResizeOptions
        //                 {
        //                     Mode = ResizeMode.Max,
        //                     Size = new SixLabors.ImageSharp.Size(150, 150)
        //                 }));

        //                 await image.SaveAsJpegAsync(thumbnailStream);
        //             }
        //         }
        //         else if (file.ContentType.StartsWith("video"))
        //         {
        //             // Placeholder for video thumbnail generation logic
        //             await GenerateVideoThumbnail(file, thumbnailStream);
        //         }

        //         thumbnailStream.Seek(0, SeekOrigin.Begin);
        //         var uploadParams = new ImageUploadParams()
        //         {
        //             File = new FileDescription(Guid.NewGuid().ToString(), thumbnailStream)
        //         };

        //         var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        //         return uploadResult.SecureUrl.ToString();
        //     }
        // }

        // private async Task GenerateVideoThumbnail(IFormFile file, Stream thumbnailStream)
        // {
        //     var placeholderImage = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(150, 150);
        //     placeholderImage.Mutate(ctx => ctx.Fill(SixLabors.ImageSharp.Color.Gray));
        //     await placeholderImage.SaveAsJpegAsync(thumbnailStream);
        // }
    }
}
