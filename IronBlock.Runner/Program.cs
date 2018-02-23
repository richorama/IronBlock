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
            try
            {
                if (!args.Any()) 
                {
                    Console.WriteLine("Specify an XML file as the first argument");
                    Environment.ExitCode = 1;
                    return;
                }

                var filename = args.First();

                if (!File.Exists(filename))
                {
                    Console.WriteLine($"ERROR: File ({filename}) does not exist");
                    Environment.ExitCode = 1;
                    return;
                }

                var xml = File.ReadAllText(filename);
                
                new Parser()
                    .AddStandardBlocks()
                    .Parse(xml)
                    .Evaluate();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.ToString()}");
                Environment.ExitCode = 1;
            }
        }
    }
}
