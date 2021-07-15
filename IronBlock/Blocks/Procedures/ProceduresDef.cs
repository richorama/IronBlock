using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
  public class ProceduresDef : IBlock
  {
    public override object Evaluate(Context context)
    {
      var name = this.Fields.Get("NAME");
      var statement = this.Statements.FirstOrDefault(x => x.Name == "STACK");

      if (string.IsNullOrWhiteSpace(name)) return null;

      // if the statement is missing, create a stub one
      if (null == statement)
      {
        statement = new Statement
        {
          Block = null,
          Name = "STACK"
        };
      }

      // tack the return value on as a block at the end of the statement
      if (this.Values.Any(x => x.Name == "RETURN"))
      {
        var valueBlock = new ValueBlock(this.Values.First(x => x.Name == "RETURN"));
        if (statement.Block == null)
        {
          statement.Block = valueBlock;
        }
        else
        {
          FindEndOfChain(statement.Block).Next = valueBlock;
        }
      }

      if (context.Functions.ContainsKey(name))
      {
        context.Functions[name] = statement;
      }
      else
      {
        context.Functions.Add(name, statement);
      }

      return null;
    }

    public override SyntaxNode Generate(Context context)
    {
      var name = this.Fields.Get("NAME").CreateValidName();
      var statement = this.Statements.FirstOrDefault(x => x.Name == "STACK");

      if (string.IsNullOrWhiteSpace(name)) return null;

      // if the statement is missing, create a stub one
      if (null == statement)
      {
        statement = new Statement
        {
          Block = null,
          Name = "STACK"
        };
      }

      ReturnStatementSyntax returnStatement = null;

      // tack the return value on as a block at the end of the statement
      if (this.Values.Any(x => x.Name == "RETURN"))
      {
        var returnValue = this.Values.First(x => x.Name == "RETURN");
        var returnExpression = returnValue.Generate(context) as ExpressionSyntax;
        if (returnExpression == null) throw new ApplicationException($"Unknown expression for return statement.");

        returnStatement = ReturnStatement(returnExpression);
      }

      var parameters = new List<ParameterSyntax>();

      var procedureContext = new ProcedureContext() { Parent = context };

      foreach (var mutation in this.Mutations.Where(x => x.Domain == "arg" && x.Name == "name"))
      {
        string parameterName = mutation.Value.CreateValidName();

        ParameterSyntax parameter = Parameter(
          Identifier(parameterName)
        )
        .WithType(
          IdentifierName("dynamic")
        );

        parameters.Add(parameter);
        procedureContext.Parameters[parameterName] = parameter;
      }

      if (statement?.Block != null)
      {
        var statementSyntax = statement.Block.GenerateStatement(procedureContext);
        if (statementSyntax != null)
        {
          procedureContext.Statements.Add(statementSyntax);
        }
      }

      if (returnStatement != null)
      {
        procedureContext.Statements.Add(returnStatement);
      }

      LocalFunctionStatementSyntax methodDeclaration = null;

      var returnType = (returnStatement == null) ? PredefinedType(Token(SyntaxKind.VoidKeyword)) : (TypeSyntax)IdentifierName("dynamic");

      methodDeclaration =
          LocalFunctionStatement(
            returnType,
            Identifier(name)
          )
          .WithBody(
            Block(procedureContext.Statements)
          );

      if (parameters.Any())
      {
        var syntaxList = SeparatedList(parameters);

        methodDeclaration =
          methodDeclaration
            .WithParameterList(
              ParameterList(syntaxList)
            );
      }

      context.Functions[name] = methodDeclaration;

      return base.Generate(context);
    }

    static IBlock FindEndOfChain(IBlock block)
    {
      if (null == block.Next) return block;
      return FindEndOfChain(block.Next);
    }


    class ValueBlock : IBlock
    {
      Value value;
      public ValueBlock(Value value)
      {
        this.value = value;
      }
      public override object Evaluate(Context context)
      {
        return this.value.Evaluate(context);
      }
    }

  }


}