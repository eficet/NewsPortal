using API.Enums;

namespace API.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NewsType NewsType { get; set; }
        public string Text { get; set; }
    }
}