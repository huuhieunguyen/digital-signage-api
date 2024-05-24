using Azure.Storage.Blobs;
using CMS.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class AzureBlobStorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("media-container");
        var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
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
    //         var containerClient = _blobServiceClient.GetBlobContainerClient("thumbnails");
    //         var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + ".jpg");

    //         await blobClient.UploadAsync(thumbnailStream, true);
    //         return blobClient.Uri.ToString();
    //     }
    // }

    // private async Task GenerateVideoThumbnail(IFormFile file, Stream thumbnailStream)
    // {
    //     var placeholderImage = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(150, 150);
    //     placeholderImage.Mutate(ctx => ctx.Fill(SixLabors.ImageSharp.Color.Gray));
    //     await placeholderImage.SaveAsJpegAsync(thumbnailStream);
    // }
}
