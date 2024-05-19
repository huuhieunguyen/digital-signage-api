using CMS.Models;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly CmsDbContext _dbContext;

        public LabelRepository(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Label>> GetAllLabels()
        {
            return await _dbContext.Labels.ToListAsync();
        }

        public async Task<Label> GetLabel(int id)
        {
            return await _dbContext.Labels.SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Label> CreateLabel(Label label)
        {
            await _dbContext.Labels.AddAsync(label);
            await _dbContext.SaveChangesAsync();
            return label;
        }

        public async Task<Label> UpdateLabel(Label label)
        {
            var existingLabel = await GetLabel(label.Id);
            if (existingLabel == null)
            {
                return null;
            }

            existingLabel.Name = label.Name;
            existingLabel.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingLabel;
        }

        public async Task DeleteLabel(Label label)
        {
            _dbContext.Labels.Remove(label);
            await _dbContext.SaveChangesAsync();
        }
    }
}