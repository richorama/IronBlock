using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsIf : IBlock
    {
        public override object Evaluate(Context context)
        {
            
            var ifCount = 1;
            if (null != this.Mutations.GetValue("elseif"))
            {
                var elseIf = this.Mutations.GetValue("elseif");
                ifCount = int.Parse(elseIf) + 1;
            }

            var done = false;
            for (var i = 0; i < ifCount; i++)
            {
                if ((bool) Values.Evaluate($"IF{i}", context))
                {
                    var statement = this.Statements.GetStatement($"DO{i}");
                    statement.Evaluate(context);
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                if (null != this.Mutations.GetValue("else"))
                {
                    var elseExists = this.Mutations.GetValue("else");
                    if (elseExists == "1")
                    {
                        var statement = this.Statements.GetStatement("ELSE");
                        statement.Evaluate(context);
                    }
                }
            }

            return base.Evaluate(context);
        }
    }

}