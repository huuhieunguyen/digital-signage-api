// using Azure.Storage.Blobs;
// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Processing;
// using CMS.Repositories;
// using CMS.Models;
// using CMS.Factories;

// namespace CMS.Services
// {
//     public class ContentItemService : IContentItemService
//     {
//         private readonly IContentItemRepository _contentItemRepository;
//         private readonly BlobServiceClient _blobServiceClient;
//         private readonly IStorageServiceFactory _storageServiceFactory;

//         public ContentItemService(IContentItemRepository contentItemRepository, IStorageServiceFactory storageServiceFactory)
//         {
//             _contentItemRepository = contentItemRepository;
//             _storageServiceFactory = storageServiceFactory;
//         }

//         public async Task<IEnumerable<ContentItem>> GetAllContentItemsAsync()
//         {
//             return await _contentItemRepository.GetAllContentItemsAsync();
//         }

//         public async Task<ContentItem> GetContentItemByIdAsync(int contentItemId)
//         {
//             return await _contentItemRepository.GetContentItemByIdAsync(contentItemId);
//         }

//         // public async Task<ContentItem> AddContentItemAsync(IFormFile file, string storageOption)
//         // {
//         //     if (file == null || (file.ContentType != "image/jpeg" && file.ContentType != "video/mp4"))
//         //     {
//         //         throw new ArgumentException("Only JPEG images and MP4 videos are allowed.");
//         //     }

//         //     var storageService = _storageServiceFactory.CreateStorageService(storageOption);

//         //     var fileUrl = await storageService.UploadFileAsync(file);

//         //     // Generate a thumbnail and upload to Azure Blob Storage, then get the URL
//         //     // var thumbnailUrl = await storageService.UploadThumbnailAsync(file);

//         //     // Extract metadata from the file
//         //     var contentItem = new ContentItem
//         //     {
//         //         Title = Path.GetFileNameWithoutExtension(file.FileName),
//         //         FilePath = fileUrl,
//         //         // ThumbnailUrl = thumbnailUrl,
//         //         ResourceType = GetResourceType(file),
//         //         Duration = file.ContentType.StartsWith("image") ? 10 : (int?)null,
//         //         Dimensions = await GetDimensionsAsync(file)
//         //     };

//         //     return await _contentItemRepository.AddContentItemAsync(contentItem);
//         // }

//         public async Task<IEnumerable<ContentItem>> AddContentItemsAsync(List<IFormFile> files, string storageOption)
//         {
//             var contentItems = new List<ContentItem>();

//             var allowedImageExtensions = new HashSet<string> { ".jpeg", ".jpg", ".png", ".gif", ".bmp", ".tiff" };
//             var allowedVideoExtensions = new HashSet<string> { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm" };

//             foreach (var file in files)
//             {
//                 var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
//                 if (!allowedImageExtensions.Contains(extension) && !allowedVideoExtensions.Contains(extension))
//                 {
//                     throw new ArgumentException("Unsupported file type.");
//                 }

//                 var storageService = _storageServiceFactory.CreateStorageService(storageOption);
//                 var fileUrl = await storageService.UploadFileAsync(file);

//                 // Generate a thumbnail and upload to Azure Blob Storage, then get the URL
//                 // var thumbnailUrl = await storageService.UploadThumbnailAsync(file);

//                 var contentItem = new ContentItem
//                 {
//                     Title = Path.GetFileNameWithoutExtension(file.FileName),
//                     FilePath = fileUrl,
//                     // ThumbnailUrl = thumbnailUrl,
//                     ResourceType = allowedImageExtensions.Contains(extension) ? ResourceType.Image : ResourceType.Video,
//                     Duration = allowedImageExtensions.Contains(extension) ? 10 : (int?)null,
//                     Dimensions = await GetDimensionsAsync(file)
//                 };

//                 contentItems.Add(contentItem);
//                 await _contentItemRepository.AddContentItemAsync(contentItem);
//             }

//             return contentItems;
//         }

//         public async Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem)
//         {
//             return await _contentItemRepository.UpdateContentItemAsync(contentItem);
//         }

//         public async Task DeleteContentItemAsync(int contentItemId)
//         {
//             await _contentItemRepository.DeleteContentItemAsync(contentItemId);
//         }

//         // private async Task<string> UploadFileToBlobStorage(IFormFile file)
//         // {
//         //     var containerClient = _blobServiceClient.GetBlobContainerClient("media-container");
//         //     var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

//         //     using (var stream = file.OpenReadStream())
//         //     {
//         //         await blobClient.UploadAsync(stream, true);
//         //     }

//         //     return blobClient.Uri.ToString();
//         // }

//         private ResourceType GetResourceType(IFormFile file)
//         {
//             return file.ContentType.StartsWith("image") ? ResourceType.Image : ResourceType.Video;
//         }

//         private async Task<string> GetDimensionsAsync(IFormFile file)
//         {
//             if (file.ContentType.StartsWith("image"))
//             {
//                 using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
//                 {
//                     return $"{image.Width}x{image.Height}";
//                 }
//             }
//             else if (file.ContentType.StartsWith("video"))
//             {
//                 // Placeholder for getting video dimensions
//                 return await GetVideoDimensions(file);
//             }

//             return null;
//         }
//         private async Task<string> GetVideoDimensions(IFormFile file)
//         {
//             // Implement logic to extract video dimensions using a library like FFmpeg
//             return "1920x1080"; // Example resolution
//         }

//         // private async Task<string> GenerateAndUploadThumbnail(IFormFile file)
//         //         {
//         //             using (var thumbnailStream = new MemoryStream())
//         //             {
//         //                 if (file.ContentType.StartsWith("image"))
//         //                 {
//         //                     using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
//         //                     {
//         //                         image.Mutate(x => x.Resize(new ResizeOptions
//         //                         {
//         //                             Mode = ResizeMode.Max,
//         //                             Size = new SixLabors.ImageSharp.Size(150, 150)
//         //                         }));

//         //                         await image.SaveAsJpegAsync(thumbnailStream);
//         //                     }
//         //                 }
//         //                 else if (file.ContentType.StartsWith("video"))
//         //                 {
//         //                     // Placeholder for video thumbnail generation logic
//         //                     await GenerateVideoThumbnail(file, thumbnailStream);
//         //                 }

//         //                 thumbnailStream.Seek(0, SeekOrigin.Begin);
//         //                 var containerClient = _blobServiceClient.GetBlobContainerClient("thumbnails");
//         //                 var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");

//         //                 await blobClient.UploadAsync(thumbnailStream, true);
//         //                 return blobClient.Uri.ToString();
//         //             }
//         //         }

//         // private async Task GenerateVideoThumbnail(IFormFile file, Stream thumbnailStream)
//         // {
//         //     // Implement logic to generate a thumbnail for video files using a library like FFmpeg
//         //     var placeholderImage = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(150, 150);
//         //     placeholderImage.Mutate(ctx => ctx.Fill(SixLabors.ImageSharp.Color.Gray));
//         //     await placeholderImage.SaveAsJpegAsync(thumbnailStream);
//         // }
//     }
// }