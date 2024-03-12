namespace kvaksy_backend.data.models
{
    public class DbChanges<T> where T : class
    {
        public T Model { get; set; }
        public int Changes { get; set; }
    }
}
