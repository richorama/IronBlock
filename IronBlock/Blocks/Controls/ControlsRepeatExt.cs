using System.Collections.Generic;

namespace IronBlock.Blocks.Controls
{
    public class ControlsRepeatExt : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var timesValue = (double) this.Values.Evaluate("TIMES", variables);

            var statement = this.Statements.GetStatement("DO");

            for (var i = 0; i < timesValue; i++)
            {
                statement.Evaluate(variables);
            }

            return base.Evaluate(variables);
        }
    }

}