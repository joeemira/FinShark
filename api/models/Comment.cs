using System;

namespace api.models
{
    public class Comment
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } =string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
