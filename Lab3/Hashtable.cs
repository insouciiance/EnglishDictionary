using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Hashtable
    {

        private LinkedList<KeyValue>[] _table;
        private int _count;

        public Hashtable(int size=5)
        {
            _table = new LinkedList<KeyValue>[size];
        }

        public int Count => _table.Length;

        public void Add(object key,object val)
        {
            if((double) _count / _table.Length > .8)
                _table = Resize();
            
            Add(_table,key,val);
            _count++;
        }
        
        public object Get(object key)
        {
            string hash = key.ToString();
            int index = HashToIndex(hash,_table.Length);
            
            if (index >= _table.Length || _table[index] == null)
                return null;

            if (_table[index].Head == _table[index].Last)
                return _table[index].Head.Data.Value;

            foreach (KeyValue node in _table[index])
            {
                if (node.Hash.Equals(hash))
                    return node.Value;
            }

            return null;
        }

        private void Add(LinkedList<KeyValue>[] table,object key,object val)
        {
            string hash = key.ToString();
            int index = HashToIndex(hash,table.Length);
            if (table[index] != null)
            {
                table[index].Add(new KeyValue(hash,val));
            }
            else
            {
                table[index] = new LinkedList<KeyValue>(new KeyValue(hash,val));
            }
        }
        
        private LinkedList<KeyValue>[] Resize()
        {
            var newTable = new LinkedList<KeyValue>[_table.Length * 2];

            foreach (var chain in _table)
            {
                if (chain?.Head != null)
                {
                    foreach (KeyValue node in chain)
                    {
                        Add(newTable,node.Hash,node.Value);
                    }
                }
            }

            return newTable;
        }

        private int HashToIndex(string hash,int arraySize)
        {
            long sum = 0;
            long mul = 1;
            hash = hash.ToLowerInvariant();
            for (int i = 0; i < hash.Length; i++) {
                mul = (i % 4 == 0) ? 1 : mul * 256;
                sum += hash[i] * mul;
            }
            return (int)(Math.Abs(sum) % arraySize);
        }

        private struct KeyValue
        {
            private string _hash;
            private object _value;

            public string Hash => _hash;
            public object Value => _value;

            public KeyValue(string hash, object value)
            {
                this._hash = hash;
                this._value = value;
            }
        }

    }
}
