using System;
using System.Collections.Generic;
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

            Console.WriteLine("Parsing the dictionary...");
            var parserTask = parser
                .ParseAsync((line) => line.Split(';')[0], line => line)
                .ContinueWith(tableTask =>
                {
                    Console.WriteLine("Parsing done.");
                    return tableTask;
                });

            string word = Console.ReadLine();

            if (!parserTask.IsCompleted)
            {
                Console.WriteLine("Waiting for the parser...");
            }

            Hashtable<string, string> dictionary = await await parserTask;

            Console.WriteLine(dictionary.Get(word?.ToUpperInvariant()));

            Console.ReadKey();
        }
    }
}
