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

                if (context.EscapeMode == EscapeMode.Break)
                {
                    context.EscapeMode = EscapeMode.None;
                    break;
                }

                context.EscapeMode = EscapeMode.None;
            }

            context.EscapeMode = EscapeMode.None;
                
            return base.Evaluate(context);
        }
    }

}