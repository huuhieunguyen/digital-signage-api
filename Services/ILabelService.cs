using CMS.Models;

namespace CMS.Services
{
    public interface ILabelService
    {
        Task<IEnumerable<Label>> GetAllLabels();
        Task<Label> GetLabel(int id);
        Task<Label> CreateLabel(Label label);
        Task<Label> UpdateLabel(Label label);
        Task DeleteLabel(int id);
    }
}