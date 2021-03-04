namespace API.Common
{
    public class MainResponse<T>
    {
        public string Path { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}