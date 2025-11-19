using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  /// <summary>
  /// Implements the procedures_ifreturn block (guard clause pattern).
  /// If the condition is true, returns the specified value from the procedure.
  /// 
  /// KNOWN BUG: When nested inside other control structures (if statements, loops, etc.),
  /// this block does not properly return from the containing procedure. The return value
  /// is lost and execution continues after the containing structure.
  /// 
  /// This works correctly at the top level of a procedure's STACK, but fails when nested.
  /// 
  /// The Generate() method produces correct code (a proper return statement), so code
  /// generation works as expected. The issue is only in the Evaluate() execution path.
  /// 
  /// Fix requires implementing EscapeMode.Return similar to Break/Continue:
  /// 1. Add EscapeMode.Return enum value
  /// 2. Add context.ReturnValue property to store the return value
  /// 3. Set context.EscapeMode = EscapeMode.Return here
  /// 4. Update all control flow blocks to check for and propagate EscapeMode.Return
  /// 
  /// See PROCEDURES_IFRETURN_BUG.md for detailed analysis and solution architecture.
  /// See ProceduresIfReturnBugTests.cs for test cases demonstrating the issue.
  /// </summary>
  public class ProceduresIfReturn : IBlock
  {
    public override object Evaluate(Context context)
    {
      var condition = this.Values.Evaluate("CONDITION", context);
      if ((bool)condition)
      {
        // Set escape mode to signal return from procedure
        context.ReturnValue = this.Values.Evaluate("VALUE", context);
        context.EscapeMode = EscapeMode.Return;
        return context.ReturnValue;
      }

      return base.Evaluate(context);
    }

    public override SyntaxNode Generate(Context context)
    {
      var condition = this.Values.Generate("CONDITION", context) as ExpressionSyntax;
      if (condition == null) throw new ApplicationException($"Unknown expression for condition.");

      ReturnStatementSyntax returnStatement = ReturnStatement();

      if (this.Values.Any(x => x.Name == "VALUE"))
      {
        var statement = this.Values.Generate("VALUE", context) as ExpressionSyntax;
        if (statement == null) throw new ApplicationException($"Unknown expression for return statement.");

        returnStatement = ReturnStatement(statement);
      }

      var ifStatement = IfStatement(condition, returnStatement);
      return Statement(ifStatement, base.Generate(context), context);
    }
  }
}