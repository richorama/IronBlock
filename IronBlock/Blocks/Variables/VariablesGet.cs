using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Variables
{
    public class VariablesGet : IBlock
    {
        public override object Evaluate(Context context)
        {
            var variableName = this.Fields.Get("VAR");

            if (!context.Variables.ContainsKey(variableName)) return null;

            return context.Variables[variableName];
        }
    }

}