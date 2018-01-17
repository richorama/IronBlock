using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Controls
{
    public class ControlsForEach : IBlock
    {
        public override object Evaluate(Context context)
        {
            var variableName = this.Fields.Get("VAR");
            var list = this.Values.Evaluate("LIST",context) as IEnumerable<object>;

            var statement = this.Statements.Where(x => x.Name == "DO").FirstOrDefault();

            if (null == statement) return base.Evaluate(context);

            foreach (var item in list)
            {
                if (context.Variables.ContainsKey(variableName))
                {
                    context.Variables[variableName] = item;
                }
                else
                {
                    context.Variables.Add(variableName, item);
                }
                statement.Evaluate(context);
            }

            return base.Evaluate(context);
        }
    }

}