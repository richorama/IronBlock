using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class ProceduresDef : IBlock
    {
        public override object Evaluate(Context context)
        {
            var name = this.Fields.Get("NAME");
            var statement = this.Statements.FirstOrDefault(x => x.Name == "STACK");

            if (string.IsNullOrWhiteSpace(name)) return null;

            // if the statement is missing, create a stub one
            if (null == statement)
            {
                statement = new Statement{
                    Block = null,
                    Name = "STACK"
                };
            }

            // tack the return value on as a block at the end of the statement
            if (this.Values.Any(x => x.Name == "RETURN"))
            {
                var valueBlock = new ValueBlock(this.Values.First(x => x.Name == "RETURN"));
                if (statement.Block == null){
                    statement.Block = valueBlock;
                }
                else 
                {
                    FindEndOfChain(statement.Block).Next = valueBlock;
                }
            }

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

        static IBlock FindEndOfChain(IBlock block)
        {
            if (null == block.Next) return block;
            return FindEndOfChain(block.Next);
        }


        class ValueBlock : IBlock
        {
            Value value;
            public ValueBlock(Value value)
            {
                this.value = value;
            }
            public override object Evaluate(Context context)
            {
                return this.value.Evaluate(context);
            }
        }

    }


}