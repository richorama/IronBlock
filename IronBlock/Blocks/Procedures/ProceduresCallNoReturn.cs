using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class ProceduresCallNoReturn : IBlock
    {
        public override object Evaluate(Context context)
        {
            // todo: add guard for missing name

            var name = this.Mutations.GetValue("name");
          
            var statement = context.Functions[name];

            var funcContext = new Context();
            funcContext.Functions = context.Functions;
            
            var counter = 0;
            foreach (var mutation in this.Mutations.Where(x => x.Domain == "arg" && x.Name == "name"))
            {   
                var value = this.Values.Evaluate($"ARG{counter}", context);
                funcContext.Variables.Add(mutation.Value, value);
                counter++;
            }

            statement.Evaluate(funcContext);

            return base.Evaluate(context);
        }
    }

}