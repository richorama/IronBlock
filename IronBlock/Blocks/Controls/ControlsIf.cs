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

			IfStatementSyntax ifStatement = null;
			IfStatementSyntax elseIfStatement = null;

			for (var i = 0; i < ifCount; i++)
			{
				var conditional = Values.Generate($"IF{i}", context) as ExpressionSyntax;
				if (conditional == null) throw new ApplicationException($"Unknown expression for condition.");

				var statement = this.Statements.Get($"DO{i}");
				var statementExp = statement.Generate(context) as ExpressionSyntax;
				if (statementExp == null) throw new ApplicationException($"Unknown expression for statement.");

				if (ifStatement == null)
				{
					ifStatement = IfStatement(conditional, ExpressionStatement(statementExp));
				}
				else
				{
					elseIfStatement = IfStatement(conditional, ExpressionStatement(statementExp));
					ifStatement.WithElse(ElseClause(elseIfStatement));
				}
			}

			string elseMutation = this.Mutations.GetValue("else");
			if (string.IsNullOrEmpty(elseMutation))
				return ifStatement;

			if (elseMutation == "1")
			{
				var statement = this.Statements.Get("ELSE");
				var elseStatement = statement.Generate(context) as ExpressionSyntax;
				if (elseStatement == null) throw new ApplicationException($"Unknown expression for else statement.");

				var latestIfStatement = elseIfStatement ?? ifStatement;
				latestIfStatement.WithElse(ElseClause(ExpressionStatement(elseStatement)));
			}

			return ifStatement;
		}
	}
}