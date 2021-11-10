namespace CoreLib.Models
{
    public abstract class PaginationResponseModelBase<T>
    {
        public long TotalItemCount { get; set; }
        public long CurrentItemCount { get; set; }
        public int From { get; set; }
        public int Size { get; set; }
        public T Data { get; set; }

        public PaginationResponseModelBase(T data, long totalItemCount, long currentItemCount, int from, int size)
        {
            TotalItemCount = totalItemCount;
            CurrentItemCount = currentItemCount;
            From = from;
            Size = size;
            Data = data;
        }
    }
}