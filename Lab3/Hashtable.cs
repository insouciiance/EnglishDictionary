using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Hashtable<TKey, TValue> where TKey : IEquatable<TKey>
    {

        private LinkedList<KeyValuePair<TKey, TValue>>[] _table;
        private int _count;

        public Hashtable(int size = 5)
        {
            _table = new LinkedList<KeyValuePair<TKey, TValue>>[size];
        }

        public int Count => _table.Length;

        public void Add(TKey key, TValue val)
        {
            if ((double)_count / _table.Length > .8)
                _table = Resize();

            Add(_table, key, val);
            _count++;
        }

        public object Get(object key)
        {
            string hash = key.ToString().ToLowerInvariant();
            int index = HashToIndex(hash, _table.Length);

            if (index >= _table.Length || _table[index] == null)
                return null;

            if (_table[index].Head == _table[index].Last)
                return _table[index].Head.Data.Value;

            foreach (KeyValuePair<TKey, TValue> node in _table[index])
            {
                if (node.Key.Equals(key))
                    return node.Value;
            }

            return null;
        }

        private void Add(LinkedList<KeyValuePair<TKey, TValue>>[] table, TKey key, TValue val)
        {
            string hash = key.ToString().ToLowerInvariant();
            int index = HashToIndex(hash, table.Length);
            if (table[index] != null)
            {
                table[index].Add(new KeyValuePair<TKey, TValue>(key, val));
            }
            else
            {
                table[index] = new LinkedList<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, val));
            }
        }

        private LinkedList<KeyValuePair<TKey, TValue>>[] Resize()
        {
            var newTable = new LinkedList<KeyValuePair<TKey, TValue>>[_table.Length * 2];

            foreach (var chain in _table)
            {
                if (chain?.Head != null)
                {
                    foreach (KeyValuePair<TKey, TValue> node in chain)
                    {
                        Add(newTable, node.Key, node.Value);
                    }
                }
            }

            return newTable;
        }

        private int HashToIndex(string hash, int arraySize)
        {
            long sum = 0;
            long mul = 1;
            for (int i = 0; i < hash.Length; i++)
            {
                mul = (i % 4 == 0) ? 1 : mul * 256;
                sum += hash[i] * mul;
            }
            return (int)(Math.Abs(sum) % arraySize);
        }
    }
}
