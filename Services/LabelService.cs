using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labelRepository;

        public LabelService(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public async Task<IEnumerable<Label>> GetAllLabels()
        {
            return await _labelRepository.GetAllLabels();
        }

        public async Task<Label> GetLabel(int id)
        {
            return await _labelRepository.GetLabel(id);
        }

        public async Task<Label> CreateLabel(Label label)
        {
            return await _labelRepository.CreateLabel(label);
        }

        public async Task<Label> UpdateLabel(Label label)
        {
            return await _labelRepository.UpdateLabel(label);
        }

        public async Task DeleteLabel(int id)
        {
            var label = await _labelRepository.GetLabel(id);
            if (label != null)
            {
                await _labelRepository.DeleteLabel(label);
            }
        }
    }
}