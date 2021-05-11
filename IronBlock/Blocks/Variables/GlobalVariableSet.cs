using Microsoft.CodeAnalysis;

namespace IronBlock.Blocks.Variables
{
  // Fast-Solution
  public class GlobalVariablesSet : IBlock
  {
    public override object Evaluate(Context context)
    {
      var value = Values.Evaluate("VALUE", context);
      var variableName = Fields.Get("VAR");

      var rootContext = context.GetRootContext();

      if (rootContext.Variables.ContainsKey(variableName))
        rootContext.Variables[variableName] = value;
      else
        rootContext.Variables.Add(variableName, value);

      return base.Evaluate(context);
    }

    public override SyntaxNode Generate(Context context) => null;
  }
}