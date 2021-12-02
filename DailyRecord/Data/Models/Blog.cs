using System;

namespace DailyRecord.Data.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Entry { get; set; }
        public DateTime DatePublish { get; set; }
    }
}
