using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
  public class ColourRgb : IBlock
  {
    Random random = new Random();
    public override object Evaluate(Context context)
    {
      var red = Convert.ToByte(this.Values.Evaluate("RED", context));
      var green = Convert.ToByte(this.Values.Evaluate("GREEN", context));
      var blue = Convert.ToByte(this.Values.Evaluate("BLUE", context));

      return $"#{red:x2}{green:x2}{blue:x2}";
    }
  }

}