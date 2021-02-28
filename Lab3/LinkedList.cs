using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; }
        private LinkedListNode<T> _last;

        public LinkedList(T initialData)
        {
            Head = new LinkedListNode<T>(initialData, null);
            _last = Head;
        }

        public void Add(T item)
        {
            _last.Next = new LinkedListNode<T>(item, null);
            _last = _last.Next;
        }
    }
}
