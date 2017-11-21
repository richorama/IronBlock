using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Variables
{
    public class VariablesSet : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var value = this.Values.Evaluate("VALUE", variables);

            var variableName = this.Fields.Evaluate("VAR");

            if (variables.ContainsKey(variableName))
            {
                variables[variableName] = value;
            }
            else
            {
                variables.Add(variableName, value);
            }

            return base.Evaluate(variables);
        }
    }

}