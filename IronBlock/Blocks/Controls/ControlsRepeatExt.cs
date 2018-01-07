using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsRepeatExt : IBlock
    {
        public override object Evaluate(Context context)
        {
            var timesValue = (double) this.Values.Evaluate("TIMES", context);

            if (!this.Statements.Any(x => x.Name == "DO")) return base.Evaluate(context);

            var statement = this.Statements.GetStatement("DO");

            for (var i = 0; i < timesValue; i++)
            {
                statement.Evaluate(context);
            }

            return base.Evaluate(context);
        }
    }

}