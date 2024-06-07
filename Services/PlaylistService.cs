using CMS.Services;
using AutoMapper;
using CMS.DTOs;
using CMS.Models;
using CMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.DTOs.PlaylistDtos;

namespace CMS.Services
{
    public interface IPlaylistService : IBaseService<PlaylistResponseDto, PlaylistCreateRequestDto>
    {
    }

    public class PlaylistService : BaseService<Playlist, PlaylistResponseDto, PlaylistCreateRequestDto>, IPlaylistService
    {
        private readonly IContentItemRepository _contentItemRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public PlaylistService(
            IPlaylistRepository repository,
            IContentItemRepository contentItemRepository,
            ILabelRepository labelRepository,
            IScheduleRepository scheduleRepository,
            IMapper mapper
        ) : base(repository, mapper)
        {
            _contentItemRepository = contentItemRepository;
            _labelRepository = labelRepository;
            _scheduleRepository = scheduleRepository;
        }

        public override async Task<PlaylistResponseDto> CreateAsync(PlaylistCreateRequestDto request)
        {
            var playlist = _mapper.Map<Playlist>(request);

            // Add content items
            if (request.ContentItemIds != null)
            {
                playlist.PlaylistContentItems = new List<PlaylistContentItem>();
                foreach (var contentItemId in request.ContentItemIds)
                {
                    var contentItem = await _contentItemRepository.GetByIdAsync(contentItemId);
                    if (contentItem != null)
                    {
                        playlist.PlaylistContentItems.Add(new PlaylistContentItem
                        {
                            Playlist = playlist,
                            ContentItem = contentItem
                        });
                    }
                }
            }

            // Add labels
            if (request.LabelIds != null)
            {
                playlist.PlaylistLabels = new List<PlaylistLabel>();
                foreach (var labelId in request.LabelIds)
                {
                    var label = await _labelRepository.GetByIdAsync(labelId);
                    if (label != null)
                    {
                        playlist.PlaylistLabels.Add(new PlaylistLabel
                        {
                            Playlist = playlist,
                            Label = label
                        });
                    }
                }
            }

            // Add schedule
            if (request.Schedule != null)
            {
                var schedule = _mapper.Map<Schedule>(request.Schedule);
                playlist.Schedules = new List<Schedule> { schedule };
            }

            var createdPlaylist = await _repository.CreateAsync(playlist);
            return _mapper.Map<PlaylistResponseDto>(createdPlaylist);
        }

        public override async Task<PlaylistResponseDto> UpdateAsync(int id, PlaylistCreateRequestDto request)
        {
            var playlist = await _repository.GetByIdAsync(id);
            if (playlist != null)
            {
                _mapper.Map(request, playlist);

                // Update content items
                playlist.PlaylistContentItems.Clear();
                if (request.ContentItemIds != null)
                {
                    foreach (var contentItemId in request.ContentItemIds)
                    {
                        var contentItem = await _contentItemRepository.GetByIdAsync(contentItemId);
                        if (contentItem != null)
                        {
                            playlist.PlaylistContentItems.Add(new PlaylistContentItem
                            {
                                Playlist = playlist,
                                ContentItem = contentItem
                            });
                        }
                    }
                }

                // Update labels
                playlist.PlaylistLabels.Clear();
                if (request.LabelIds != null)
                {
                    foreach (var labelId in request.LabelIds)
                    {
                        var label = await _labelRepository.GetByIdAsync(labelId);
                        if (label != null)
                        {
                            playlist.PlaylistLabels.Add(new PlaylistLabel
                            {
                                Playlist = playlist,
                                Label = label
                            });
                        }
                    }
                }

                // Update schedule
                playlist.Schedules.Clear();
                if (request.Schedule != null)
                {
                    var schedule = _mapper.Map<Schedule>(request.Schedule);
                    playlist.Schedules.Add(schedule);
                }

                await _repository.UpdateAsync(playlist);
                return _mapper.Map<PlaylistResponseDto>(playlist);
            }
            return null;
        }
    }
}
