using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class UsdPriceExpression(ValueOperator oper, decimal value) 
        : KeyValueExpression("usd", oper, value.ToString("F2"))
    {
    }
}
