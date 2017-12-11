using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextPrompt : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var inputType = "TEXT";
            this.Mutations.TryGetValue("type", out inputType);

            var text = (this.Values.Evaluate("TEXT", variables) ?? "").ToString();

            if (!string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine(text);
            }

            var value = Console.ReadLine();

            if (inputType == "NUMBER")
            {
                return int.Parse(value);                
            }

            return text;
        }
    }

}