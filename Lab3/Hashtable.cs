using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Hashtable<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const double MaxLoadFactor = .8;

        private LinkedList<KeyValuePair<TKey, TValue>>[] _table;
        private int _count;

        private Func<TKey, int> _hashFunction;

        private double LoadFactor => (double) _count / _table.Length;

        public Hashtable(Func<TKey, int> hashFunction, int size = 5)
        {
            _table = new LinkedList<KeyValuePair<TKey, TValue>>[size];
            _hashFunction = hashFunction;
        }

        public void Add(TKey key, TValue val)
        {
            if (LoadFactor > MaxLoadFactor)
            {
                Resize();
            }

            int index = HashToIndex(key, _table.Length);
            if (_table[index] != null)
            {
                _table[index].Add(new KeyValuePair<TKey, TValue>(key, val));
            }
            else
            {
                _table[index] = new LinkedList<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, val));
            }

            _count++;
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = HashToIndex(key, _table.Length);

                if (key == null)
                {
                    throw new ArgumentNullException();
                }

                if (index >= _table.Length || _table[index] == null)
                {
                    throw new KeyNotFoundException();
                }

                if (_table[index].Head == _table[index].Last && key.Equals(_table[index].Head.Data.Key))
                {
                    return _table[index].Head.Data.Value;
                }

                foreach (KeyValuePair<TKey, TValue> node in _table[index])
                {
                    if (node.Key.Equals(key))
                    {
                        return node.Value;
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        public string HashTablesInfo()
        {
            string result = "";
            result += $"Elements count: {_count}\n";
            result += $"Table lenght: {_table.Length}\n";
            result += $"Loadfactor: {LoadFactor}\n";

            double averageBlockLoad = 0;
            int maxBlockLoad = 0;
            
            foreach (var block in _table)
            {
                if(block == null)
                    continue;

                averageBlockLoad += block.Count;
                if (block.Count > maxBlockLoad)
                    maxBlockLoad = block.Count;
            }

            averageBlockLoad /= _table.Length;

            result += $"Average Block Load: {averageBlockLoad}\n";
            result += $"Max Block Load: {maxBlockLoad}\n";
            return result;
        }

        private void Resize(int multiplier = 2)
        {
            var oldTable = _table;
            _table = new LinkedList<KeyValuePair<TKey, TValue>>[_table.Length * multiplier];
            _count = 0;
            
            foreach (var chain in oldTable)
            {
                if (chain?.Head != null)
                {
                    foreach (KeyValuePair<TKey, TValue> node in chain)
                    {
                        Add(node.Key, node.Value);
                    }
                }
            }
        }

        private int HashToIndex(TKey o, int arraySize)
        {
            return Math.Abs(_hashFunction(o)) % arraySize;
        }
    }
}
