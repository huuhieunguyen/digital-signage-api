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

            CreateMap<Schedule, ScheduleResponseDto>().ReverseMap();
            CreateMap<ScheduleCreateRequestDto, Schedule>().ReverseMap();

            CreateMap<ContentItem, ContentItemResponseDto>().ReverseMap();
            CreateMap<ContentItemCreateRequestDto, ContentItem>()
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore());
            CreateMap<ContentItemUpdateRequestDto, ContentItem>()
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore());

            CreateMap<Playlist, PlaylistResponseDto>()
                .ForMember(dest => dest.ContentItems, opt => opt.MapFrom(src => src.PlaylistContentItems.Select(pci => pci.ContentItem).ToList()))
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.PlaylistLabels.Select(pl => pl.Label).ToList()))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule));
            CreateMap<PlaylistCreateRequestDto, Playlist>()
                .ForMember(dest => dest.PlaylistLabels, opt => opt.MapFrom(src => src.LabelIds.Select(labelId => new PlaylistLabel { LabelId = labelId }).ToList()))
                .ForMember(dest => dest.PlaylistContentItems, opt => opt.MapFrom(src => src.ContentItemIds.Select(contentItemId => new PlaylistContentItem { ContentItemId = contentItemId }).ToList()))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => new Schedule { StartTime = src.Schedule.StartTime, EndTime = src.Schedule.EndTime, DaysOfWeek = src.Schedule.DaysOfWeek }));

            // CreateMap<PlaylistCreateRequestDto, Playlist>()
            //     .ForMember(dest => dest.PlaylistContentItems, opt => opt.Ignore())
            //     .ForMember(dest => dest.PlaylistLabels, opt => opt.Ignore())
            //     .ForMember(dest => dest.Schedule, opt => opt.Ignore());

            CreateMap<Label, LabelResponseDto>().ReverseMap();
            CreateMap<LabelCreateRequestDto, Label>()
                .ForMember(dest => dest.PlaylistLabels, opt => opt.Ignore())
                .ForMember(dest => dest.PlayerLabels, opt => opt.Ignore());
        }
    }
}