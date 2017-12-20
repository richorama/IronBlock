using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
    public class ColourRgb : IBlock
    {
        Random random = new Random();
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var red = Convert.ToByte(this.Values.Evaluate("RED", variables));
            var green = Convert.ToByte(this.Values.Evaluate("GREEN", variables));
            var blue = Convert.ToByte(this.Values.Evaluate("BLUE", variables));

            return $"#{red:x2}{green:x2}{blue:x2}";
        }
    }

}