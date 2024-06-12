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
        Task<List<PlaylistResponseDto>> GetPlaylistsByLabelNameAsync(string labelName);
    }

    public class PlaylistService : BaseService<Playlist, PlaylistResponseDto, PlaylistCreateRequestDto>, IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository repository, IMapper mapper)
        : base(repository, mapper)
        {
            _playlistRepository = repository;
        }

        public async Task<List<PlaylistResponseDto>> GetPlaylistsByLabelNameAsync(string labelName)
        {
            var playlists = await _playlistRepository.GetPlaylistsByLabelNameAsync(labelName);
            return _mapper.Map<List<PlaylistResponseDto>>(playlists);
        }

        public override async Task<PlaylistResponseDto> CreateAsync(PlaylistCreateRequestDto request)
        {
            var playlist = new Playlist
            {
                Title = request.Title,
                PlaylistLabels = request.LabelIds.Select(labelId => new PlaylistLabel { LabelId = labelId }).ToList(),
                PlaylistContentItems = request.ContentItemIds.Select(contentItemId => new PlaylistContentItem { ContentItemId = contentItemId }).ToList(),
                Schedule = new Schedule
                {
                    StartTime = request.Schedule.StartTime,
                    EndTime = request.Schedule.EndTime,
                    DaysOfWeek = request.Schedule.DaysOfWeek
                }
            };

            var createdPlaylist = await _repository.CreateAsync(playlist);
            return _mapper.Map<PlaylistResponseDto>(createdPlaylist);
        }

        public override async Task<PlaylistResponseDto> UpdateAsync(int id, PlaylistCreateRequestDto request)
        {
            var playlist = await _playlistRepository.GetByIdAsync(id);
            if (playlist == null)
            {
                return null;
            }

            playlist.Title = request.Title;
            playlist.PlaylistLabels = request.LabelIds.Select(labelId => new PlaylistLabel { PlaylistId = id, LabelId = labelId }).ToList();
            playlist.PlaylistContentItems = request.ContentItemIds.Select(contentItemId => new PlaylistContentItem { PlaylistId = id, ContentItemId = contentItemId }).ToList();
            playlist.Schedule.StartTime = request.Schedule.StartTime;
            playlist.Schedule.EndTime = request.Schedule.EndTime;
            playlist.Schedule.DaysOfWeek = request.Schedule.DaysOfWeek;
            playlist.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(playlist);
            return _mapper.Map<PlaylistResponseDto>(playlist);
        }
    }
}
