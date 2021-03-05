using API.Enums;

namespace API.Services.DTOs
{
    public class CreateNewsDto
    {
        public string Title { get; set; }
        public NewsType NewsType { get; set; }
        public string Text { get; set; } }
}