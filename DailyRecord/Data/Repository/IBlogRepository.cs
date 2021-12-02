using DailyRecord.Data.Models;

namespace DailyRecord.Repository
{
    public interface IBlogRepository
    {
        void AddEntry(Blog model);
    }
}