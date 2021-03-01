using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class LinkedList<T> : IEnumerable<T>
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

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new LinkedListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        private class LinkedListEnumerator : IEnumerator<T>
        {
            public T Current => CurentNode.Data;
            
            private LinkedListNode<T> Head;
            private LinkedListNode<T> CurentNode;
            object IEnumerator.Current => Current;

            private bool _firstNodeVisited = false;

            public LinkedListEnumerator(LinkedList<T> head)
            {
                Head = head.Head;
                CurentNode = Head;
            }

            public void Dispose() { }
            public bool MoveNext()
            {
                if (!_firstNodeVisited)
                {
                    _firstNodeVisited = true;
                    return true;
                }
                
                if (CurentNode.Next != null)
                {
                    CurentNode = CurentNode.Next;
                    return true;
                }

                return false;
            }
            public void Reset()
            {
                _firstNodeVisited = false;
                CurentNode = Head;
            }
        }
    }
}
