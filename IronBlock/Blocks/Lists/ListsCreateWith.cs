using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronBlock.Blocks.Lists
{
    public class ListsCreateWith : IBlock
    {
        public override object Evaluate(Context context)
        {
            var list = new List<object>();
            foreach (var value in this.Values)
            {
                list.Add(value.Evaluate(context));

            }
            return list;

         }
    }
}