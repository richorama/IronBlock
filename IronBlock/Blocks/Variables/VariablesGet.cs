using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Variables
{
    public class VariablesGet : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var variableName = this.Fields.Evaluate("VAR");

            if (!variables.ContainsKey(variableName)) return null;

            return variables[variableName];
        }
    }

}