using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Tests
{

  internal static class TestExtensions
  {
    internal class DebugPrint : IBlock
    {
      public static List<string> Text { get; set; }

      static DebugPrint()
      {
        Text = new List<string>();
      }

      public override object Evaluate(Context context)
      {
        Text.Add((this.Values.First(x => x.Name == "TEXT").Evaluate(context) ?? "").ToString());
        return base.Evaluate(context);
      }
    }

    internal static IList<string> GetDebugText()
    {
      return DebugPrint.Text;
    }

    internal static IParser AddDebugPrinter(this IParser parser)
    {
      DebugPrint.Text.Clear();

      parser.AddBlock<DebugPrint>("text_print");
      return parser;
    }

  }
}