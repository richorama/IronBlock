using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
    public class ColourRandom : IBlock
    {
        Random random = new Random();
        public override object Evaluate(IDictionary<string, object> variables)
        {
            var bytes = new byte[3];
            random.NextBytes(bytes);
            
            var hex = new StringBuilder(bytes.Length * 2);
            hex.Append("#");
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            
            return hex.ToString(); // between #000000 - #FFFFFF
        }
    }

}