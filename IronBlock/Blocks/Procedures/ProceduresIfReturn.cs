using System;
using System.Collections.Generic;
using System.Linq;

namespace IronBlock.Blocks.Text
{
    public class ProceduresIfReturn : IBlock
    {
        public override object Evaluate(Context context)
        {
            var condition = this.Values.Evaluate("CONDITION", context);
            if ((bool) condition)
            {
                return this.Values.Evaluate("VALUE", context);
            }

            return base.Evaluate(context);
        }

    }


}