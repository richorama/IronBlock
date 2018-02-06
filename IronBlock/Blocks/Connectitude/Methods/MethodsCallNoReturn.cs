using IronBlock.Blocks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Connectitude.Methods
{
	public class MethodsCallNoReturn : IBlock
	{
		public override SyntaxNode Generate(Context context)
		{
			var name = this.Mutations.GetValue("name");

			if (!context.Functions.ContainsKey(name)) throw new MissingMethodException($"Method ${name} not defined");

			var statement = context.Functions[name];

			var funcContext = new Context();
			funcContext.Functions = context.Functions;

			var counter = 0;
			foreach (var mutation in this.Mutations.Where(x => x.Domain == "arg" && x.Name == "name"))
			{
				var value = this.Values.Generate($"ARG{counter}", context);
				funcContext.Variables.Add(mutation.Value, value);
				counter++;
			}

			//statement.Generate(funcContext);
			var methodInvocation = InvocationExpression(
										IdentifierName("Parse")
									)
									.WithArgumentList(
										ArgumentList(
											SingletonSeparatedList(
												Argument(
													funcContext.Variables.First().Value as ExpressionSyntax
												)
											)
										)
									);

			context.Statements.Add(ExpressionStatement(methodInvocation));

			return base.Generate(context);
		}
	}
}