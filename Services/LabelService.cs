using AutoMapper;
using CMS.DTOs.LabelDtos;
using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public interface ILabelService : IBaseService<LabelResponseDto, LabelCreateRequestDto>
    {
    }

    public class LabelService : BaseService<Label, LabelResponseDto, LabelCreateRequestDto>, ILabelService
    {
        public LabelService(ILabelRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

}