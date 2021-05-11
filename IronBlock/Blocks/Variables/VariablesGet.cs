using System;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Variables
{
  public class VariablesGet : IBlock
  {
    public override object Evaluate(Context context)
    {
      var variableName = this.Fields.Get("VAR");

      // Fast-Solution
      if (!context.Variables.ContainsKey(variableName))
      {
        if (!context.GetRootContext().Variables.ContainsKey(variableName))
          return null;

        return context.GetRootContext().Variables[variableName];
      }

      return context.Variables[variableName];
    }

    public override SyntaxNode Generate(Context context)
    {
      var variableName = this.Fields.Get("VAR").CreateValidName();

      return IdentifierName(variableName);
    }
  }

}