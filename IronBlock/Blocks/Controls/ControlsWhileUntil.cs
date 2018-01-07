using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsWhileUntil : IBlock
    {
        public override object Evaluate(Context context)
        {
            var mode = this.Fields.Get("MODE");
            var value = this.Values.FirstOrDefault(x => x.Name == "BOOL");
            
            if (!this.Statements.Any(x => x.Name == "DO") || null == value) return base.Evaluate(context);
            
            var statement = this.Statements.GetStatement("DO");

            if (mode == "WHILE")
            {
                while((bool) value.Evaluate(context))
                {
                    statement.Evaluate(context);
                }
            }
            else
            {
                while(!(bool) value.Evaluate(context))
                {
                    statement.Evaluate(context);
                }
            }

            return base.Evaluate(context);
        }
    }

}