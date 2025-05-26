namespace AdminApp.Utils.Models
{
    public class ResultOperation
    {
        public bool StateOperation { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class ResultOperation<T> : ResultOperation
    {
        public T? Result { get; set; }
        public List<T>? Results { get; set; }
    }
}