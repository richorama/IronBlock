using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class ColourPicker : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            return this.Fields.Evaluate("COLOUR") ?? "#000000";
        }
    }

}