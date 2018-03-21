using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Text
{
    public class TextJoin : IBlock
    {
        public override object Evaluate(Context context)
        {
            var items = int.Parse(this.Mutations.GetValue("items"));

            var sb = new StringBuilder();
            for (var i = 0; i < items; i++)
            {
                if (!this.Values.Any(x => x.Name == $"ADD{i}")) continue;
                sb.Append(this.Values.Evaluate($"ADD{i}", context));
            }

            return sb.ToString();
        }

		public override SyntaxNode Generate(Context context)
		{
			var items = int.Parse(this.Mutations.GetValue("items"));

			return this.Values.LastOrDefault()?.Generate(context);
		}
	}

}