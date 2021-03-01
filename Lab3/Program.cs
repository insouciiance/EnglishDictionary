using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DictionaryParser parser = new DictionaryParser("../../../dictionary.txt");
            Hashtable dictionary = await parser.ParseAsync((line) => line.Split(';')[0]);

            Console.WriteLine(dictionary.Get("abaCUs"));

            Console.ReadKey();
        }
    }
}
