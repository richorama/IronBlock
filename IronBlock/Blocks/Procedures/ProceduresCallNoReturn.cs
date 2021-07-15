using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class ProceduresCallNoReturn : IBlock
  {
    public override object Evaluate(Context context)
    {
      // todo: add guard for missing name

      var name = this.Mutations.GetValue("name");

      if (!context.Functions.ContainsKey(name)) throw new MissingMethodException($"Method ${name} not defined");

      var statement = (IFragment)context.Functions[name];

      var funcContext = new Context() { Parent = context };
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

    public override SyntaxNode Generate(Context context)
    {
      var methodName = Mutations.GetValue("name").CreateValidName();

      var arguments = new List<ArgumentSyntax>();

      var counter = 0;
      foreach (var mutation in Mutations.Where(x => x.Domain == "arg" && x.Name == "name"))
      {
        var argumentExpression = this.Values.Generate($"ARG{counter}", context) as ExpressionSyntax;
        if (argumentExpression == null)
          throw new ApplicationException($"Unknown argument expression for ARG{counter}.");

        arguments.Add(Argument(argumentExpression));
        counter++;
      }

      var methodInvocation =
        InvocationExpression(
          IdentifierName(methodName)
        );


      if (arguments.Any())
      {
        var syntaxList = SeparatedList(arguments);

        methodInvocation =
          methodInvocation
            .WithArgumentList(
              ArgumentList(
                syntaxList
              )
            );
      }

      return Statement(methodInvocation, base.Generate(context), context);
    }
  }

}