using System;

namespace GrainMaster.Models
{
    public class NewsRepository
    {
        public string Title { get; set; }
        public string FeedType { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
    }

    public class NewsModel
    {
        public int Count { get; set; }
        public string link { get; set; }
    }
}