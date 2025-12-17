using System.Collections.Generic;
using System.Linq;

namespace App5
{
    public class Queue<T>
    {
        private List<T> _items = new List<T>();

        public void Enqueue(T item) => _items.Add(item);

        public T Dequeue()
        {
            if (_items.Count == 0) return default;
            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }

        public T Peek() => _items.Count > 0 ? _items[0] : default;
        public IEnumerable<T> GetItems() => _items.AsEnumerable();
        public int Count => _items.Count;
    }
}
