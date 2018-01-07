using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Tests
{

    internal static class TestExtensions
    {
        internal class DebugPrint : IBlock
        {
            public List<string> Text { get; set; }

            public DebugPrint()
            {
                this.Text = new List<string>();
            }

            public override object Evaluate(Context context)
            {
                this.Text.Add((this.Values.First(x => x.Name == "TEXT").Evaluate(context) ?? "").ToString());
                return base.Evaluate(context);
            }
        }

        internal static DebugPrint AddDebugPrinter(this Parser parser)
        {
            var printer = new DebugPrint();
            parser.AddBlock("text_print", () => printer);
            return printer;
        }

    }
}