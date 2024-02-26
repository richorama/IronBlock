using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IronBlock;
using IronBlock.Blocks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace IronBlock.BlazorSample
{

  internal class CustomPrintBlock : IBlock
  {
    public List<string> Text { get; set; } = new List<string>();

    public override object Evaluate(Context context)
    {
      Text.Add((this.Values.FirstOrDefault(x => x.Name == "TEXT")?.Evaluate(context) ?? "").ToString());
      return base.Evaluate(context);
    }
  }

  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      await builder.Build().RunAsync();
    }

    [JSInvokable]
    public static string Evaluate(string xml)
    {
      var printBlock = new CustomPrintBlock();

      new IronBlock.XmlParser()
        .AddStandardBlocks()
        .AddBlock("text_print", printBlock)
        .Parse(xml)
        .Evaluate();

      return string.Join('\n', printBlock.Text);

    }
  }
}
