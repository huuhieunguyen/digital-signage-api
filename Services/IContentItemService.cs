using CMS.DTOs.ContentItemDtos;
using CMS.Models;
using CMS.Services;

namespace CMS.Services
{
    public interface IContentItemService : IBaseService<ContentItemResponseDto, ContentItemCreateRequestDto>
    {
        Task<IEnumerable<ContentItemResponseDto>> UploadContentItemsAsync(IEnumerable<IFormFile> files);
        Task<ContentItemResponseDto> UpdateContentItemAsync(int id, ContentItemUpdateRequestDto request);
        Task<FileDownloadResult> DownloadContentItemAsync(int id);
    }
}