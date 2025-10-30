namespace ViewModel.Infrastructure
{
    public class Result<T> where T : class
    {
        public bool Success { get; set; }
        public int Count { get; set; }
        public T Entity { get; set; }
        public List<T> Entities { get; set; }
        public string Message { get; set; }
    }
}
