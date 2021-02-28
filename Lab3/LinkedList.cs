using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class LinkedList<T> : IEnumerable
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
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }
        
        private class LinkedListEnumerator<T> : IEnumerator<T>
        {


            public T Current => CurentNode.Data;
            
            private LinkedListNode<T> Head;
            private LinkedListNode<T> CurentNode;
            object IEnumerator.Current => Current;

            private bool firstNode;

            public LinkedListEnumerator(LinkedList<T> head)
            {
                Head = head.Head;
                CurentNode = Head;
                firstNode = true;
            }

            public void Dispose() { }
            public bool MoveNext()
            {
                if (firstNode)
                {
                    firstNode = false;
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
                firstNode = true;
                CurentNode = Head;
            }
            
        }
    }
}
