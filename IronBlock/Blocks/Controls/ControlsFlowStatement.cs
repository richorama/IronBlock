using System;
using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Controls
{
  public class ControlsFlowStatement : IBlock
  {
    public override object Evaluate(Context context)
    {
      var flow = this.Fields.Get("FLOW");
      if (flow == "CONTINUE")
      {
        context.EscapeMode = EscapeMode.Continue;
        return null;
      }

      if (flow == "BREAK")
      {
        context.EscapeMode = EscapeMode.Break;
        return null;
      }

      throw new NotSupportedException($"{flow} flow is not supported");
    }

    public override SyntaxNode Generate(Context context)
    {
      var flow = this.Fields.Get("FLOW");
      if (flow == "CONTINUE")
      {
        return ContinueStatement();
      }

      if (flow == "BREAK")
      {
        return BreakStatement();
      }

      throw new NotSupportedException($"{flow} flow is not supported");
    }
  }

}