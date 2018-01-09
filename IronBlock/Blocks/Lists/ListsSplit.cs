using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Lists
{
    public class ListsSplit : IBlock
    {
        public override object Evaluate(Context context)
        {
            var mode = this.Fields.Get("MODE");
            var input = this.Values.Evaluate("INPUT", context);
            var delim = this.Values.Evaluate("DELIM", context);

            switch (mode)
            {
                case "SPLIT":
                    return input
                        .ToString()
                        .Split(new string[] {delim.ToString() }, StringSplitOptions.None)
                        .ToList();

                case "JOIN":
                    return string
                        .Join(delim.ToString(), (input as IEnumerable<object>).Select(x => x.ToString()));

                default:
                    throw new NotSupportedException($"unknown mode: {mode}");

            }
        }
    }
}