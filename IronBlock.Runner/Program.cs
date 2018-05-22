using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace IronBlock.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2) 
                {
                    Console.WriteLine(
@"Specify an XML file as the first argument

Specify any of the following as a second argument
  -e (evaluate)
  -g (generate)
  -c (compile)
  -ex (execute)
");
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

				var parser = 
					new Parser()
						.AddStandardBlocks()
						.Parse(xml);

				var mode = args.Skip(1).FirstOrDefault();
				if (mode?.Equals("-g") ?? false)
				{
					var syntaxTree = parser.Generate();
					string code = syntaxTree.NormalizeWhitespace().ToFullString();
					Console.WriteLine(code);
				}
				else if (mode?.Equals("-c") ?? false)
				{
					var syntaxTree = parser.Generate();
					string code = syntaxTree.NormalizeWhitespace().ToFullString();
					var script = GenerateScript(code);

					Console.WriteLine("Compiling...");
					
					var diagnostics = Compile(script);
					Console.WriteLine("Compile result:");

					if (!diagnostics.Any())
					{
						Console.WriteLine("OK");
					}
					else
					{
						foreach (var diagnostic in diagnostics)
						{
							Console.WriteLine(diagnostic.GetMessage());
						}
					}
				}
				else if (mode?.Equals("-ex") ?? false)
				{
					var syntaxTree = parser.Generate();
					string code = syntaxTree.NormalizeWhitespace().ToFullString();
					var script = GenerateScript(code);

					Console.WriteLine("Executing script...");
					ExecuteAsync(script).Wait();
				}
				else
				{
					parser.Evaluate();
				}

				Console.ReadKey();
			}
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.ToString()}");
                Environment.ExitCode = 1;
            }
        }

		public static IEnumerable<Diagnostic> Compile(Script<object> script)
		{
			if (script == null)
				return Enumerable.Empty<Diagnostic>();

			try
			{
				return script.Compile();
			}
			catch (CompilationErrorException compilationErrorException)
			{
				return compilationErrorException.Diagnostics;
			}
		}

		public static Script<object> GenerateScript(string code)
		{
			var dynamicRuntimeReference = MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).Assembly.Location);
			var runtimeBinderReference = MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location);

			var scriptOptions =
					ScriptOptions.Default
						.WithImports("System", "System.Linq", "System.Math")
						.AddReferences(dynamicRuntimeReference, runtimeBinderReference);

			return CSharpScript.Create<object>(code, scriptOptions);
		}

		public static async Task<object> ExecuteAsync(Script<object> script)
		{
			var scriptState = await script.RunAsync();
			return scriptState.ReturnValue;
		}
	}
}
