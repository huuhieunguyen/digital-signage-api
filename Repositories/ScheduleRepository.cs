using CMS.Data;
using CMS.Models;

namespace CMS.Repositories
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
    }

    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(CmsDbContext context) : base(context)
        {
        }
    }

}