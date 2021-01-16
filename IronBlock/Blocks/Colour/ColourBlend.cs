using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
  public class ColourBlend : IBlock
  {
    Random random = new Random();
    public override object Evaluate(Context context)
    {
      var colour1 = (this.Values.Evaluate("COLOUR1", context) ?? "").ToString();
      var colour2 = (this.Values.Evaluate("COLOUR2", context) ?? "").ToString();
      var ratio = System.Math.Min(System.Math.Max((double)this.Values.Evaluate("RATIO", context), 0), 1);

      if (string.IsNullOrWhiteSpace(colour1) || colour1.Length != 7) return null;
      if (string.IsNullOrWhiteSpace(colour2) || colour2.Length != 7) return null;

      var red = (byte)((double)Convert.ToByte(colour1.Substring(1, 2), 16) * (1 - ratio) + (double)Convert.ToByte(colour2.Substring(1, 2), 16) * ratio);
      var green = (byte)((double)Convert.ToByte(colour1.Substring(3, 2), 16) * (1 - ratio) + (double)Convert.ToByte(colour2.Substring(3, 2), 16) * ratio);
      var blue = (byte)((double)Convert.ToByte(colour1.Substring(5, 2), 16) * (1 - ratio) + (double)Convert.ToByte(colour2.Substring(5, 2), 16) * ratio);

      return $"#{red:x2}{green:x2}{blue:x2}";
    }
  }

}