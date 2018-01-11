using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Lists
{
    public class ListsRepeat : IBlock
    {
        public override object Evaluate(Context context)
        {
            var item = this.Values.Evaluate("ITEM", context);
            var num = (double) this.Values.Evaluate("NUM", context);

            var list = new List<object>();
            for (var i = 0; i < num; i++)
            {
                list.Add(item);

            }
            return list;

         }
    }
}