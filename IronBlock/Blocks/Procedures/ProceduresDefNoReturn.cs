using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class ProceduresDefNoReturn : IBlock
    {
        public override object Evaluate(Context context)
        {
            var name = this.Fields.Get("NAME");
            var statement = this.Statements.FirstOrDefault(x => x.Name == "STACK");

            if (string.IsNullOrWhiteSpace(name) || statement == null) return null;

            if (context.Functions.ContainsKey(name))
            {
                context.Functions[name] = statement;
            }
            else
            {
                context.Functions.Add(name, statement);
            }

            return null;

        }
    }

}