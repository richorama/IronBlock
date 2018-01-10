using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Lists
{
    public class ListsLength : IBlock
    {
        public override object Evaluate(Context context)
        {
            var value = this.Values.Evaluate("VALUE", context) as IEnumerable<object>;
            if (null == value) return 0.0;

            return (double) value.Count();
        }
    }
}