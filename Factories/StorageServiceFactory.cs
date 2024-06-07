// using System;
// using CMS.Services;

// namespace CMS.Factories
// {
//     // You can add any other storage service by implementing the IStorageService interface and updating this StorageServiceFactory 
//     public interface IStorageServiceFactory
//     {
//         IStorageService CreateStorageService(string storageOption);
//     }

//     public class StorageServiceFactory : IStorageServiceFactory
//     {
//         private readonly IServiceProvider _serviceProvider;

//         public StorageServiceFactory(IServiceProvider serviceProvider)
//         {
//             // _serviceProvider = serviceProvider;
//             _serviceProvider = serviceProvider;
//         }

//         public IStorageService CreateStorageService(string storageOption)
//         {
//             var storageService = storageOption switch
//             {
//                 "azure" => (IStorageService)_serviceProvider.GetService(typeof(AzureBlobStorageService)),
//                 "cloudinary" => (IStorageService)_serviceProvider.GetService(typeof(CloudinaryStorageService)),
//                 _ => throw new ArgumentException("Invalid storage option specified."),
//             };

//             if (storageService == null)
//             {
//                 throw new InvalidOperationException("Failed to create storage service.");
//             }

//             return storageService;

//             //     return storageOption.ToLower() switch
//             // {
//             //     "azure" => _serviceProvider.GetRequiredService<AzureBlobStorageService>(),
//             //     "cloudinary" => _serviceProvider.GetRequiredService<CloudinaryStorageService>(),
//             //     _ => throw new InvalidOperationException("Failed to create storage service.")
//             // };
//         }
//     }

// }