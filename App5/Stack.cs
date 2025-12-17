using System.Collections.Generic;
using System.Linq;

namespace App5
{
    public class Stack<T>
    {
        private List<T> _items = new List<T>();

        public void Push(T item) => _items.Add(item);

        public T Pop()
        {
            if (_items.Count == 0) return default;
            T item = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }

        public T Peek() => _items.Count > 0 ? _items[_items.Count - 1] : default;
        public IEnumerable<T> GetItems() => _items.AsEnumerable();
        public int Count => _items.Count;
    }
}
