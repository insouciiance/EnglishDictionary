using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head { get; private set; }
        public LinkedListNode<T> Last { get; private set; }

        public LinkedList(T initialData)
        {
            Head = new LinkedListNode<T>(initialData, null);
            Last = Head;
        }

        public LinkedList() { }

        public void Add(T item)
        {
            if (Last == null)
            {
                Head = new LinkedListNode<T>(item, null);
                Last = Head;
                return;
            }
            
            Last.Next = new LinkedListNode<T>(item, null);
            Last = Last.Next;
        }
    }
}
