using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Controls
{
	public class ControlsIf : IBlock
	{
		public override object Evaluate(Context context)
		{

			var ifCount = 1;
			if (null != this.Mutations.GetValue("elseif"))
			{
				var elseIf = this.Mutations.GetValue("elseif");
				ifCount = int.Parse(elseIf) + 1;
			}

			var done = false;
			for (var i = 0; i < ifCount; i++)
			{
				if ((bool)Values.Evaluate($"IF{i}", context))
				{
					var statement = this.Statements.Get($"DO{i}");
					statement.Evaluate(context);
					done = true;
					break;
				}
			}

			if (!done)
			{
				if (null != this.Mutations.GetValue("else"))
				{
					var elseExists = this.Mutations.GetValue("else");
					if (elseExists == "1")
					{
						var statement = this.Statements.Get("ELSE");
						statement.Evaluate(context);
					}
				}
			}

			return base.Evaluate(context);
		}

		public override SyntaxNode Generate(Context context)
		{
			var ifCount = 1;
			string elseifMutation = this.Mutations.GetValue("elseif");
			if (!string.IsNullOrEmpty(elseifMutation))
			{
				var elseIf = elseifMutation;
				ifCount = int.Parse(elseIf) + 1;
			}

			var ifStatements = new List<IfStatementSyntax>();

			for (var i = 0; i < ifCount; i++)
			{
				var conditional = Values.Generate($"IF{i}", context) as ExpressionSyntax;
				if (conditional == null) throw new ApplicationException($"Unknown expression for condition.");

				var statement = this.Statements.Get($"DO{i}");
				var statementSyntax = statement.GenerateStatement(context);
				if (statementSyntax == null) throw new ApplicationException($"Unknown expression for statement.");
				
				var newIfStatement = IfStatement(conditional, statementSyntax);
				ifStatements.Add(newIfStatement);
			}

			string elseMutation = this.Mutations.GetValue("else");
			if (elseMutation == "1")
			{
				var statement = this.Statements.Get("ELSE");
				var elseStatement = statement.Generate(context) as ExpressionSyntax;
				if (elseStatement == null) throw new ApplicationException($"Unknown expression for else statement.");

				int lastIndex = ifStatements.Count - 1;
				if (lastIndex >= 0)
				{
					var lastIfStatement = ifStatements[lastIndex];
					ifStatements[lastIndex] = lastIfStatement.WithElse(ElseClause(ExpressionStatement(elseStatement)));
				}
			}

			for (int index = ifStatements.Count - 1; index >= 0; index--)
			{
				var currentIfStatement = ifStatements[index];
				if (index > 0)
				{
					var previousIfStatement = ifStatements[index - 1];
					ifStatements[index - 1] = previousIfStatement.WithElse(ElseClause(currentIfStatement));
				}		
			}


			var ifStatement = ifStatements.FirstOrDefault();

			var next = base.Generate(context);
			if (next == null)
				return ifStatement;

			context.Statements.Add(ifStatement);
			return next;
		}
	}
}