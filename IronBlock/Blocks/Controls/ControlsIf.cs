using System.Collections.Generic;

namespace IronBlock.Blocks.Controls
{
    public class ControlsIf : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            
            var ifCount = 1;
            if (this.Mutations.TryGetValue("elseif", out var elseIf))
            {
                ifCount = int.Parse(elseIf) + 1;
            }

            var done = false;
            for (var i = 0; i < ifCount; i++)
            {
                if ((bool) Values.Evaluate($"IF{i}", variables))
                {
                    var statement = this.Statements.GetStatement($"DO{i}");
                    statement.Evaluate(variables);
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                if (this.Mutations.TryGetValue("else", out var elseExists))
                {
                    if (elseExists == "1")
                    {
                        var statement = this.Statements.GetStatement("ELSE");
                        statement.Evaluate(variables);
                    }
                }
            }

            return base.Evaluate(variables);
        }
    }

}