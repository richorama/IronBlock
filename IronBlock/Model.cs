using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock
{
    public interface IFragment
    { 
        // probably need a method like this here:
        object Evaluate(Context context);
		SyntaxNode Generate(Context context);
	}

    public class Workspace : IFragment
    {
        public Workspace()
        {
            this.Blocks = new List<IBlock>();
        }

        public IList<IBlock> Blocks {get;set;}

		public virtual object Evaluate(Context context)
        {   
            // TODO: variables
            object returnValue = null;

            foreach (var block in this.Blocks)
            {
                returnValue = block.Evaluate(context);
            }

            return returnValue;
        }

		public virtual SyntaxNode Generate(Context context)
		{
			foreach (var block in this.Blocks)
			{
				var syntaxNode = block.Generate(context);
				if (syntaxNode == null)
					continue;

				var statement = syntaxNode as StatementSyntax;
				if (statement == null)
				{
					statement = ExpressionStatement(syntaxNode as ExpressionSyntax);
				}

				context.Statements.Insert(0, statement);
			}

			foreach (var variable in context.Variables)
			{
				var variableDeclaration = GenerateVariableDeclaration(variable.Key);
				context.Statements.Insert(0, variableDeclaration);
			}

			var blockSyntax = Block(context.Statements);		
			return blockSyntax;
		}

		private LocalDeclarationStatementSyntax GenerateVariableDeclaration(string variableName)
		{
			return LocalDeclarationStatement(
						VariableDeclaration(
							IdentifierName("dynamic")
						)
						.WithVariables(
							SingletonSeparatedList(
								VariableDeclarator(
									Identifier(variableName)
								)
							)
						)
					);
		}
	}

    public abstract class IBlock : IFragment
    { 
        public IBlock()
        {
            this.Fields = new List<Field>();
            this.Values = new List<Value>();
            this.Statements = new List<Statement>();
            this.Mutations = new List<Mutation>();
        }

        public string Id { get; set; }
        public IList<Field> Fields { get; set; }
        public IList<Value> Values { get; set; }
        public IList<Statement> Statements { get; set; }
        public string Type { get; set; }
        public bool Inline { get; set; }
        public IBlock Next { get; set; }
        public IList<Mutation> Mutations { get; set; }
        public virtual object Evaluate(Context context)
        {
            if (null != this.Next && context.EscapeMode == EscapeMode.None)
            {
                return this.Next.Evaluate(context);                
            }
            return null;
        }

		public virtual SyntaxNode Generate(Context context)
		{
			if (null != this.Next && context.EscapeMode == EscapeMode.None)
			{
				return this.Next.Generate(context);
			}
			return null;
		}
	}

    public class Statement : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(Context context)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(context);
        }
		public SyntaxNode Generate(Context context)
		{
			if (null == this.Block) return null;
			return this.Block.Generate(context);
		}
	}

    public class Value : IFragment
    { 
        public string Name { get; set; }
        public IBlock Block { get; set; }
        public object Evaluate(Context context)
        {
            if (null == this.Block) return null;
            return this.Block.Evaluate(context);
        }
		public SyntaxNode Generate(Context context)
		{
			if (null == this.Block) return null;
			return this.Block.Generate(context);
		}
	}

    public class Field
    { 
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public enum EscapeMode
    {
        None,
        Break,
        Continue
    }


    public class Context
    {
        public Context()
        {
            this.Variables = new Dictionary<string,object>();
            this.Functions = new Dictionary<string,IFragment>();

			this.Statements = new List<StatementSyntax>();
		}

		public IDictionary<string, object> Variables { get; set; }

        public IDictionary<string, IFragment> Functions { get; set; }

        public EscapeMode EscapeMode { get; set; }
        		
		public List<StatementSyntax> Statements { get; }

        public Context Parent { get; set; }
    }

    public class Mutation
    {
        public Mutation(string domain, string name, string value)
        {
            this.Domain = domain;
            this.Name = name;
            this.Value = value;
        }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }


}
