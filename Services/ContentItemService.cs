using Azure.Storage.Blobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using CMS.Repositories;
using CMS.Models;

namespace CMS.Services
{
    public class ContentItemService : IContentItemService
    {
        private readonly IContentItemRepository _contentItemRepository;
        private readonly BlobServiceClient _blobServiceClient;

        public ContentItemService(IContentItemRepository contentItemRepository, BlobServiceClient blobServiceClient)
        {
            _contentItemRepository = contentItemRepository;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<IEnumerable<ContentItem>> GetAllContentItemsAsync()
        {
            return await _contentItemRepository.GetAllContentItemsAsync();
        }

        public async Task<ContentItem> GetContentItemByIdAsync(int contentItemId)
        {
            return await _contentItemRepository.GetContentItemByIdAsync(contentItemId);
        }

        public async Task<ContentItem> AddContentItemAsync(IFormFile file)
        {
            if (file == null || (file.ContentType != "image/jpeg" && file.ContentType != "video/mp4"))
            {
                throw new ArgumentException("Only JPEG images and MP4 videos are allowed.");
            }

            // Upload the file to Azure Blob Storage and get the URL
            var fileUrl = await UploadFileToBlobStorage(file);

            // Generate a thumbnail and upload to Azure Blob Storage, then get the URL
            // var thumbnailUrl = await GenerateAndUploadThumbnail(file);

            // Extract metadata from the file
            var contentItem = new ContentItem
            {
                Title = Path.GetFileNameWithoutExtension(file.FileName),
                FilePath = fileUrl,
                // ThumbnailUrl = thumbnailUrl,
                ResourceType = GetResourceType(file),
                Duration = file.ContentType.StartsWith("image") ? 10 : (int?)null,
                Dimensions = await GetDimensionsAsync(file)
            };

            return await _contentItemRepository.AddContentItemAsync(contentItem);
        }

        public async Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem)
        {
            return await _contentItemRepository.UpdateContentItemAsync(contentItem);
        }

        public async Task DeleteContentItemAsync(int contentItemId)
        {
            await _contentItemRepository.DeleteContentItemAsync(contentItemId);
        }

        private async Task<string> UploadFileToBlobStorage(IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("media-container");
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        private ResourceType GetResourceType(IFormFile file)
        {
            return file.ContentType.StartsWith("image") ? ResourceType.Image : ResourceType.Video;
        }

        private async Task<string> GetDimensionsAsync(IFormFile file)
        {
            if (file.ContentType.StartsWith("image"))
            {
                using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                {
                    return $"{image.Width}x{image.Height}";
                }
            }
            else if (file.ContentType.StartsWith("video"))
            {
                // Placeholder for getting video dimensions
                return await GetVideoDimensions(file);
            }

            return null;
        }
        private async Task<string> GetVideoDimensions(IFormFile file)
        {
            // Implement logic to extract video dimensions using a library like FFmpeg
            return "1920x1080"; // Example resolution
        }

        // private async Task<string> GenerateAndUploadThumbnail(IFormFile file)
        //         {
        //             using (var thumbnailStream = new MemoryStream())
        //             {
        //                 if (file.ContentType.StartsWith("image"))
        //                 {
        //                     using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
        //                     {
        //                         image.Mutate(x => x.Resize(new ResizeOptions
        //                         {
        //                             Mode = ResizeMode.Max,
        //                             Size = new SixLabors.ImageSharp.Size(150, 150)
        //                         }));

        //                         await image.SaveAsJpegAsync(thumbnailStream);
        //                     }
        //                 }
        //                 else if (file.ContentType.StartsWith("video"))
        //                 {
        //                     // Placeholder for video thumbnail generation logic
        //                     await GenerateVideoThumbnail(file, thumbnailStream);
        //                 }

        //                 thumbnailStream.Seek(0, SeekOrigin.Begin);
        //                 var containerClient = _blobServiceClient.GetBlobContainerClient("thumbnails");
        //                 var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");

        //                 await blobClient.UploadAsync(thumbnailStream, true);
        //                 return blobClient.Uri.ToString();
        //             }
        //         }

        // private async Task GenerateVideoThumbnail(IFormFile file, Stream thumbnailStream)
        // {
        //     // Implement logic to generate a thumbnail for video files using a library like FFmpeg
        //     var placeholderImage = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(150, 150);
        //     placeholderImage.Mutate(ctx => ctx.Fill(SixLabors.ImageSharp.Color.Gray));
        //     await placeholderImage.SaveAsJpegAsync(thumbnailStream);
        // }
    }
}