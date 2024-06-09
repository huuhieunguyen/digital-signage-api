using AutoMapper;
using CMS.DTOs.ContentItemDtos;
using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public interface IContentItemService : IBaseService<ContentItemResponseDto, ContentItemCreateRequestDto>
    {
        Task<IEnumerable<ContentItemResponseDto>> UploadContentItemsAsync(IEnumerable<IFormFile> files);
        Task<ContentItemResponseDto> UpdateContentItemAsync(int id, ContentItemUpdateRequestDto request);
    }

    public class ContentItemService : BaseService<ContentItem, ContentItemResponseDto, ContentItemCreateRequestDto>, IContentItemService
    {
        private readonly IStorageService _storageService;
        private readonly IContentItemRepository _contentItemrepository;

        public ContentItemService(IContentItemRepository repository,
            IContentItemRepository contentItemrepository,
            IMapper mapper,
            IStorageService storageService)
            : base(repository, mapper)
        {
            _storageService = storageService;
            _contentItemrepository = contentItemrepository;
        }

        public async Task<IEnumerable<ContentItemResponseDto>> UploadContentItemsAsync(IEnumerable<IFormFile> files)
        {
            var contentItems = new List<ContentItem>();

            foreach (var file in files)
            {
                var (url, duration, width, height, dimensions) = await _storageService.UploadFileAsync(file);
                var contentItem = new ContentItem
                {
                    Title = file.FileName,
                    FilePath = url,
                    Duration = duration,
                    Width = width,
                    Height = height,
                    Dimensions = dimensions,
                    ResourceType = file.ContentType.Contains("image") ? ResourceType.Image : ResourceType.Video
                };
                contentItems.Add(contentItem);
            }

            await _contentItemrepository.CreateRangeAsync(contentItems);
            return _mapper.Map<IEnumerable<ContentItemResponseDto>>(contentItems);
        }

        public override async Task<ContentItemResponseDto> CreateAsync(ContentItemCreateRequestDto request)
        {
            var (url, duration, width, height, dimensions) = await _storageService.UploadFileAsync(request.File);
            var contentItem = new ContentItem
            {
                Title = request.Title,
                Description = request.Description,
                FilePath = url,
                Duration = duration,
                Width = width,
                Height = height,
                Dimensions = dimensions,
                ResourceType = request.File.ContentType.Contains("image") ? ResourceType.Image : ResourceType.Video
            };

            var createdContentItem = await _repository.CreateAsync(contentItem);
            return _mapper.Map<ContentItemResponseDto>(createdContentItem);
        }

        public async Task<ContentItemResponseDto> UpdateContentItemAsync(int id, ContentItemUpdateRequestDto request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _mapper.Map(request, entity);
                await _repository.UpdateAsync(entity);
                return _mapper.Map<ContentItemResponseDto>(entity);
            }
            return default;
        }

        // Download file
        public async Task<(string, string)> DownloadFileAsync(int id)
        {
            var contentItem = await _repository.GetByIdAsync(id);
            if (contentItem != null)
            {
                var fileStream = await _storageService.DownloadFileAsync(contentItem.FilePath);
                return (contentItem.Title, fileStream);
            }
            return default;
        }

    }
}
