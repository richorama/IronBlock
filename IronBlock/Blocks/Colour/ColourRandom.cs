using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Text
{
  public class ColourRandom : IBlock
  {
    Random random = new Random();
    public override object Evaluate(Context context)
    {
      var bytes = new byte[3];
      random.NextBytes(bytes);

      return string.Format("#{0}", string.Join("", bytes.Select(x => string.Format("{0:x2}", x))));
    }
  }

}