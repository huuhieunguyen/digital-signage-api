using CMS.Models;

namespace CMS.Repositories
{
    public interface ILabelRepository
    {
        Task<IEnumerable<Label>> GetAllLabels();
        Task<Label> GetLabel(int id);
        Task<Label> CreateLabel(Label label);
        Task<Label> UpdateLabel(Label label);
        Task DeleteLabel(Label label);
    }
}