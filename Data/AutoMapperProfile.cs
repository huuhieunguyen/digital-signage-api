using AutoMapper;
using CMS.DTOs.PlayerDtos;
using CMS.DTOs.ScheduleDtos;
using CMS.Models;

namespace CMS.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PlayerCreateDto, Player>();
            CreateMap<Player, PlayerResponseDto>()
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.PlayerLabels));
            CreateMap<PlayerLabel, PlayerLabelDto>();

            // CreateMap<Player, PlayerResponseDto>()
            //     .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.PlayerLabels.Select(pl => new PlayerLabelDto
            //     {
            //         Id = pl.Label.Id,
            //         Name = pl.Label.Name
            //     })); // Map Player to PlayerResponseModel

            CreateMap<Schedule, ScheduleResponseDto>().ReverseMap();
            CreateMap<ScheduleCreateRequestDto, Schedule>();

        }
    }
}