using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class TextCaseChange : IBlock
    {
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var toCase = this.Fields.Evaluate("CASE").ToString();
            var text = (this.Values.Evaluate("TEXT", variables) ?? "").ToString();

            switch(toCase)
            {
                case "UPPERCASE": 
                    return text.ToUpper(); 

                case "LOWERCASE":
                    return text.ToLower();

                case "TITLECASE":
                {
                    var textInfo = new CultureInfo("en-US",false).TextInfo;
                    return textInfo.ToTitleCase(text);
                }

                default: 
                    throw new NotSupportedException("unknown case");

            }

        }
    }

}