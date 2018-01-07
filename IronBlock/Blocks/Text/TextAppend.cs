using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextAppend : IBlock
    {
        public override object Evaluate(Context context)
        {
            var variables = context.Variables;

            var variableName = this.Fields.Get("VAR");
            var textToAppend = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

            if (!variables.ContainsKey(variableName))
            {
                variables.Add(variableName, "");
            }
            var value = variables[variableName].ToString();

            variables[variableName] = value + textToAppend;

            return base.Evaluate(context);           
        }
    }

}