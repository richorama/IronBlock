using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextPrompt : IBlock
    {
        public override object Evaluate(Context context)
        {
            var inputType = this.Mutations.GetValue("type") ?? "TEXT";

            var text = (this.Values.Evaluate("TEXT", context) ?? "").ToString();

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