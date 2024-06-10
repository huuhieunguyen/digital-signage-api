using AutoMapper;
using CMS.DTOs.ContentItemDtos;
using CMS.DTOs.LabelDtos;
using CMS.DTOs.PlayerDtos;
using CMS.DTOs.PlaylistDtos;
using CMS.DTOs.ScheduleDtos;
using CMS.Models;

namespace CMS.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, PlayerResponseDto>()
                .ForMember(dest => dest.Labels, opt
                => opt.MapFrom(src => src.PlayerLabels.Select(pl => pl.Label).ToList()));
            CreateMap<PlayerCreateRequestDto, Player>()
                .ForMember(dest => dest.PlayerLabels, opt
                => opt.MapFrom(src => src.LabelIds.Select(labelId => new PlayerLabel { LabelId = labelId }).ToList()));

            CreateMap<Label, LabelResponseDto>().ReverseMap();

            CreateMap<Schedule, ScheduleResponseDto>().ReverseMap();
            CreateMap<ScheduleCreateRequestDto, Schedule>().ReverseMap();

            CreateMap<ContentItem, ContentItemResponseDto>().ReverseMap();
            CreateMap<ContentItemCreateRequestDto, ContentItem>()
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore());
            CreateMap<ContentItemUpdateRequestDto, ContentItem>()
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore());

            // CreateMap<PlaylistContentItem, PlaylistContentItemDto>().ReverseMap();

            CreateMap<Playlist, PlaylistResponseDto>().ReverseMap();
            CreateMap<PlaylistCreateRequestDto, Playlist>()
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistLabels, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());

            CreateMap<Label, LabelResponseDto>().ReverseMap();
            CreateMap<LabelCreateRequestDto, Label>()
                .ForMember(dest => dest.PlaylistLabels, opt => opt.Ignore())
                .ForMember(dest => dest.PlayerLabels, opt => opt.Ignore());
        }
    }
}