using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Variables
{
    public class VariablesSet : IBlock
    {
        public override object Evaluate(Context context)
        {
            var variables = context.Variables;

            var value = this.Values.Evaluate("VALUE", context);

            var variableName = this.Fields.Get("VAR");

            if (variables.ContainsKey(variableName))
            {
                variables[variableName] = value;
            }
            else
            {
                variables.Add(variableName, value);
            }

            return base.Evaluate(context);
        }
    }

}