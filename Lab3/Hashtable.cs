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

        public Hashtable(int size=5)
        {
            _table = new LinkedList<KeyValue>[size];
        }

        public void Add(object key,object val)
        {
            string hash = key.ToString();
            int index = HashToIndex(hash,_table.Length);
            if (_table[index] != null)
            {
                _table[index].Add(new KeyValue(hash,val));
            }
            else
            {
                _table[index] = new LinkedList<KeyValue>(new KeyValue(hash,val));
            }
        }

        public object Get(object key)
        {
            string hash = key.ToString();
            int index = HashToIndex(hash,_table.Length);
            
            if (index >= _table.Length || _table[index] == null)
                return null;

            if (_table[index].Head == _table[index].Last)
                return _table[index].Head.Data.Value;
            
            var node = _table[index].Head;
            do
            {
                if (node.Data.Hash.Equals(hash))
                    return node.Data.Value;
                node = node.Next;
            } while (node != null);

            return null;
        }
        

        private int HashToIndex(string hash,int arraySize)
        {
            long sum = 0;
            long mul = 1;
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
