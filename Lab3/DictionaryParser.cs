using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class DictionaryParser
    {
        private readonly string _path;

        public DictionaryParser(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at {path}");
            }

            _path = path;
        }

        public async Task<Hashtable<TKey, TValue>> ParseAsync<TKey, TValue>(Func<string, TKey> keySelector, Func<string, TValue> valueSelector) where TKey : IEquatable<TKey>
        {
            Hashtable<TKey, TValue> table = new Hashtable<TKey, TValue>();

            using (StreamReader reader = new StreamReader(_path))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    table.Add(keySelector(line), valueSelector(line));
                }
            }

            return table;
        }
    }
}
