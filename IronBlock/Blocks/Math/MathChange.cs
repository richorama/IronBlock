using System;

namespace IronBlock.Blocks.Math
{
  public class MathChange : IBlock
  {
    public override object Evaluate(Context context)
    {
      var variables = context.Variables;
      var variableName = this.Fields.Get("VAR");
      var delta = (double)this.Values.Evaluate("DELTA", context);

      double value;
      if (variables.ContainsKey(variableName) && variables[variableName] != null)
      {
        value = (double)variables[variableName];
      }
      else
      {
        var rootContext = context.GetRootContext();
        
        if (rootContext.Variables.ContainsKey(variableName) && rootContext.Variables[variableName] != null)
        {
          value = (double)rootContext.Variables[variableName];
          value += delta;
          rootContext.Variables[variableName] = value;
          return base.Evaluate(context);
        }
        else
        {
          // Initialize undeclared or null variable to 0.0
          value = 0.0;
        }
      }

      value += delta;
      variables[variableName] = value;

      return base.Evaluate(context);

    }




  }

}