using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsFor : IBlock
    {
        public override object Evaluate(Context context)
        {
            var variableName = this.Fields.Get("VAR");
            
            var fromValue = (double) this.Values.Evaluate("FROM", context);
            var toValue = (double) this.Values.Evaluate("TO", context);
            var byValue = (double) this.Values.Evaluate("BY", context);
            
            var statement = this.Statements.FirstOrDefault();


            if (context.Variables.ContainsKey(variableName))
            {
                context.Variables[variableName] = fromValue;
            }
            else
            {
                context.Variables.Add(variableName, fromValue);
            }


            while ((double) context.Variables[variableName] <= toValue)
            {
                statement.Evaluate(context);
                context.Variables[variableName] = (double) context.Variables[variableName] + byValue;
            }

            return base.Evaluate(context);
        }
    }

}