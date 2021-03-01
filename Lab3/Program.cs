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
                        return tableTask.Result;
                    }, TaskContinuationOptions.ExecuteSynchronously);

            string word = Console.ReadLine()?.ToUpperInvariant();

            if (!parserTask.IsCompleted)
            {
                Console.WriteLine("Waiting for the parser...");
            }

            Hashtable<string, string> dictionary = await parserTask;

            Console.WriteLine(dictionary[word?.ToUpperInvariant()]);

            Console.ReadKey();
        }
    }
}
