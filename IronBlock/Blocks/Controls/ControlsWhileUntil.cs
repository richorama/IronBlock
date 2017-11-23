using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsWhileUntil : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var mode = this.Fields.Evaluate("MODE");
            var value = this.Values.FirstOrDefault(x => x.Name == "BOOL");
            
            if (!this.Statements.Any(x => x.Name == "DO") || null == value) return base.Evaluate(variables);
            
            var statement = this.Statements.GetStatement("DO");

            if (mode == "WHILE")
            {
                while((bool) value.Evaluate(variables))
                {
                    statement.Evaluate(variables);
                }
            }
            else
            {
                while(!(bool) value.Evaluate(variables))
                {
                    statement.Evaluate(variables);
                }
            }

            return base.Evaluate(variables);
        }
    }

}