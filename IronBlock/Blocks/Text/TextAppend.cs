using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextAppend : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var variableName = this.Fields.Evaluate("VAR");
            var textToAppend = (this.Values.Evaluate("TEXT", variables) ?? "").ToString();

            if (!variables.ContainsKey(variableName))
            {
                variables.Add(variableName, "");
            }
            var value = variables[variableName].ToString();

            variables[variableName] = value + textToAppend;

            return base.Evaluate(variables);           
        }
    }

}