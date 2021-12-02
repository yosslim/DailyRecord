using DailyRecord.Data;
using DailyRecord.Data.Models;
using System.Threading.Tasks;

namespace DailyRecord.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogRepository(ApplicationDbContext dc)
        {
            _db = dc;
        }

        public void AddEntry(Blog model)
        {
            Blog newBlog = new()
            {
                Id = model.Id,
                Entry = model.Entry,
                DatePublish = model.DatePublish
            };

            _db.Add(newBlog);
            _db.SaveChanges();

        }


    }
}
