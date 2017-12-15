using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
    public class TextJoin : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var items = int.Parse(this.Mutations["items"]);

            var sb = new StringBuilder();
            for (var i = 0; i < items; i++)
            {
                if (!this.Values.Any(x => x.Name == $"ADD{i}")) continue;
                sb.Append(this.Values.Evaluate($"ADD{i}", variables));
            }

            return sb.ToString();
        }
    }

}