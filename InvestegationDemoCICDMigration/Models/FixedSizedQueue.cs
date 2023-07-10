using System.Collections.Concurrent;

namespace DevOpsWebApp.Models
{
    public class FixedSizedQueue<T>
    {
        private readonly object lockObject = new object();

        public int Limit { get; set; }

        public ConcurrentQueue<T> Q { get; set; } = new();
    }
}
