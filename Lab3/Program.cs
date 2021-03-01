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
            Task<Hashtable<string, string>> parserTask = parser.ParseAsync((line) => line.Split(';')[0], line => line);

            Console.Write("Enter your word to get the definition: ");
            string word = Console.ReadLine();

            if (!parserTask.IsCompleted)
            {
                Console.WriteLine("Waiting for the parser...");
            }

            Hashtable<string, string> dictionary = await parserTask;
            Console.WriteLine("Parsing done.");

            Console.WriteLine(dictionary.Get(word));

            Console.ReadKey();
        }
    }
}
