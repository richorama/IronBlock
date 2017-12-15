using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextIndexOf : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var mode = this.Fields.Evaluate("END");

            var text = (this.Values.Evaluate("VALUE", variables) ?? "").ToString();
            var term = (this.Values.Evaluate("FIND", variables) ?? "").ToString();

            switch (mode)
            {
                case "FIRST": return text.IndexOf(term) + 1;
                case "LAST": return text.LastIndexOf(term) + 1;
                default: throw new ApplicationException("unknown mode");
            }

        }
    }

}