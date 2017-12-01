using System;
using System.Collections.Generic;
using System.Linq;
using IronBlock.Blocks.Math;
using IronBlock.Blocks.Text;
using IronBlock.Blocks.Variables;
using IronBlock.Blocks.Controls;
using IronBlock.Blocks.Logic;

namespace IronBlock.Blocks
{
    public static class Extensions
    {
        internal static object Evaluate(this IEnumerable<Value> values, string name, IDictionary<string, object> variables)
        {
            var value = values.FirstOrDefault(x => x.Name == name);
            if (null == value) throw new ArgumentException($"value {name} not found");
            
            return value.Evaluate(variables);
        }

        internal static string Evaluate(this IEnumerable<Field> fields, string name)
        {
            var field = fields.FirstOrDefault(x => x.Name == name);
            if (null == field) throw new ArgumentException($"field {name} not found");
            
            return field.Value;
        }

        internal static Statement GetStatement(this IEnumerable<Statement> statements, string name)
        {
            var statement = statements.FirstOrDefault(x => x.Name == name);
            if (null == statement) throw new ArgumentException($"statement {name} not found");

            return statement;
        }


        public static object Evaluate(this Workspace workspace)
        {
            return workspace.Evaluate(new Dictionary<string,object>());
        }

        public static Parser AddStandardBlocks(this Parser parser)
        {
            parser.AddBlock<ControlsRepeatExt>("controls_repeat_ext");
            parser.AddBlock<ControlsIf>("controls_if");
            parser.AddBlock<ControlsWhileUntil>("controls_whileUntil");

            parser.AddBlock<LogicCompare>("logic_compare");
            parser.AddBlock<LogicBoolean>("logic_boolean");
            parser.AddBlock<LogicNegate>("logic_negate");
            parser.AddBlock<LogicOperation>("logic_operation");
            parser.AddBlock<LogicNull>("logic_null");
            parser.AddBlock<LogicTernary>("logic_ternary");

            parser.AddBlock<MathArithmetic>("math_arithmetic");
            parser.AddBlock<MathNumber>("math_number");
            parser.AddBlock<MathSingle>("math_single");
            parser.AddBlock<MathSingle>("math_trig");
            parser.AddBlock<MathSingle>("math_round");
            parser.AddBlock<MathConstant>("math_constant");

            parser.AddBlock<TextBlock>("text");
            parser.AddBlock<TextPrint>("text_print");
            parser.AddBlock<TextLength>("text_length");

            parser.AddBlock<VariablesGet>("variables_get");
            parser.AddBlock<VariablesSet>("variables_set");

            return parser;
        }


    }

}