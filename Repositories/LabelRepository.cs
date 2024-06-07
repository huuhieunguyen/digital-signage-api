using CMS.Models;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public interface ILabelRepository : IBaseRepository<Label>
    {
    }

    public class LabelRepository : BaseRepository<Label>, ILabelRepository
    {
        public LabelRepository(CmsDbContext context) : base(context)
        {
        }
    }
}