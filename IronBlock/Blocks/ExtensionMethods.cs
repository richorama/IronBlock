using System;
using System.Collections.Generic;
using System.Linq;
using IronBlock.Blocks.Controls;
using IronBlock.Blocks.Lists;
using IronBlock.Blocks.Logic;
using IronBlock.Blocks.Math;
using IronBlock.Blocks.Text;
using IronBlock.Blocks.Variables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IronBlock.Blocks
{
  public static class Extensions
  {
    public static object Evaluate(this IEnumerable<Value> values, string name, Context context)
    {
      var value = values.FirstOrDefault(x => x.Name == name);
      if (null == value) throw new ArgumentException($"value {name} not found");

      return value.Evaluate(context);
    }

    public static SyntaxNode Generate(this IEnumerable<Value> values, string name, Context context)
    {
      var value = values.FirstOrDefault(x => x.Name == name);
      if (null == value) throw new ArgumentException($"value {name} not found");

      return value.Generate(context);
    }

    public static string Get(this IEnumerable<Field> fields, string name)
    {
      var field = fields.FirstOrDefault(x => x.Name == name);
      if (null == field) throw new ArgumentException($"field {name} not found");

      return field.Value;
    }

    public static Statement Get(this IEnumerable<Statement> statements, string name)
    {
      var statement = statements.FirstOrDefault(x => x.Name == name);
      if (null == statement) throw new ArgumentException($"statement {name} not found");

      return statement;
    }

    public static string GetValue(this IList<Mutation> mutations, string name, string domain = "mutation")
    {
      var mut = mutations.FirstOrDefault(x => x.Domain == domain && x.Name == name);
      if (null == mut) return null;
      return mut.Value;
    }

    public static object Evaluate(this Workspace workspace, IDictionary<string, object> arguments = null)
    {
      var ctx = new Context();
      if (null != arguments)
      {
        ctx.Variables = arguments;
      }
      return workspace.Evaluate(ctx);
    }

    public static SyntaxNode Generate(this Workspace workspace)
    {
      var context = new Context();
      return workspace.Generate(context);
    }

    public static StatementSyntax GenerateStatement(this IFragment fragment, Context context)
    {
      var syntaxNode = fragment.Generate(context);

      var statementSyntax = syntaxNode as StatementSyntax;
      if (statementSyntax != null)
        return statementSyntax;

      var expressionSyntax = syntaxNode as ExpressionSyntax;
      if (expressionSyntax != null)
        return SyntaxFactory.ExpressionStatement(expressionSyntax);

      return null;
    }

    public static Context GetRootContext(this Context context)
    {
      var parentContext = context?.Parent;

      while (parentContext != null)
      {
        if (parentContext.Parent == null)
          return parentContext;

        parentContext = parentContext.Parent;
      };

      return context;
    }
    internal static string CreateValidName(this string name)
    {
      return name?.Replace(" ", "_");
    }

    public static Parser AddStandardBlocks(this Parser parser)
    {
      parser.AddBlock<ControlsRepeatExt>("controls_repeat_ext");
      parser.AddBlock<ControlsIf>("controls_if");
      parser.AddBlock<ControlsWhileUntil>("controls_whileUntil");
      parser.AddBlock<ControlsFlowStatement>("controls_flow_statements");
      parser.AddBlock<ControlsForEach>("controls_forEach");
      parser.AddBlock<ControlsFor>("controls_for");

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
      parser.AddBlock<MathRound>("math_round");
      parser.AddBlock<MathConstant>("math_constant");
      parser.AddBlock<MathNumberProperty>("math_number_property");
      parser.AddBlock<MathOnList>("math_on_list");
      parser.AddBlock<MathConstrain>("math_constrain");
      parser.AddBlock<MathModulo>("math_modulo");
      parser.AddBlock<MathRandomFloat>("math_random_float");
      parser.AddBlock<MathRandomInt>("math_random_int");
      parser.AddBlock<MathChange>("math_change");

      parser.AddBlock<TextBlock>("text");
      parser.AddBlock<TextPrint>("text_print");
      parser.AddBlock<TextPrompt>("text_prompt_ext");
      parser.AddBlock<TextLength>("text_length");
      parser.AddBlock<TextIsEmpty>("text_isEmpty");
      parser.AddBlock<TextTrim>("text_trim");
      parser.AddBlock<TextCaseChange>("text_changeCase");
      parser.AddBlock<TextAppend>("text_append");
      parser.AddBlock<TextJoin>("text_join");
      parser.AddBlock<TextIndexOf>("text_indexOf");

      parser.AddBlock<VariablesGet>("variables_get");
      parser.AddBlock<VariablesSet>("variables_set");

      parser.AddBlock<ColourPicker>("colour_picker");
      parser.AddBlock<ColourRandom>("colour_random");
      parser.AddBlock<ColourRgb>("colour_rgb");
      parser.AddBlock<ColourBlend>("colour_blend");

      parser.AddBlock<ProceduresDef>("procedures_defnoreturn");
      parser.AddBlock<ProceduresDef>("procedures_defreturn");
      parser.AddBlock<ProceduresCallNoReturn>("procedures_callnoreturn");
      parser.AddBlock<ProceduresCallReturn>("procedures_callreturn");
      parser.AddBlock<ProceduresIfReturn>("procedures_ifreturn");

      parser.AddBlock<ListsSplit>("lists_split");
      parser.AddBlock<ListsCreateWith>("lists_create_with");
      parser.AddBlock<ListsLength>("lists_length");
      parser.AddBlock<ListsRepeat>("lists_repeat");
      parser.AddBlock<ListsIsEmpty>("lists_isEmpty");
      parser.AddBlock<ListsGetIndex>("lists_getIndex");
      parser.AddBlock<ListsSetIndex>("lists_setIndex");
      parser.AddBlock<ListsIndexOf>("lists_indexOf");

      return parser;
    }
  }

}