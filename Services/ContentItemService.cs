using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using CMS.Models;
using CMS.Repositories;
using Microsoft.AspNetCore.Http;

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

        public async Task<ContentItem> AddContentItemAsync(ContentItem contentItem, IFormFile file)
        {
            // Upload the file to Azure Blob Storage
            var filePath = await UploadFileToBlobStorage(file);

            // Generate a thumbnail and upload to Azure Blob Storage
            var thumbnailUrl = await GenerateAndUploadThumbnail(file);

            contentItem.FilePath = filePath;
            contentItem.ThumbnailUrl = thumbnailUrl;

            if (contentItem.ResourceType == ResourceType.Image)
            {
                contentItem.Duration = contentItem.Duration ?? 10;
            }

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
            var containerClient = _blobServiceClient.GetBlobContainerClient("content-items");
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        private async Task<string> GenerateAndUploadThumbnail(IFormFile file)
        {
            // Logic to generate a thumbnail from the file
            // This depends on the type of the file (image/video) and the specific implementation

            // Placeholder: Generate a thumbnail and return its URL
            var thumbnailKey = "path/to/thumbnail";

            // Upload the generated thumbnail to Azure Blob Storage
            var containerClient = _blobServiceClient.GetBlobContainerClient("thumbnails");
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");

            // Placeholder: Generate a thumbnail stream and upload
            using (var thumbnailStream = new MemoryStream())
            {
                // Generate thumbnail logic here

                await blobClient.UploadAsync(thumbnailStream, true);
            }

            return blobClient.Uri.ToString();
        }
    }

}
