using System;
using System.IO;
using System.Linq;
using IronBlock.Blocks;

namespace IronBlock.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any()) 
            {
                Console.WriteLine("Specify an XML file as the first argument");
                return;
            }

            var filename = args.First();

            if (!File.Exists(filename))
            {
                Console.WriteLine($"Error, file ({filename}) does not exist");
                return;
            }

            var xml = File.ReadAllText(filename);
            
            new Parser()
                .AddStandardBlocks()
                .Parse(xml)
                .Evaluate();

        }
    }
}
