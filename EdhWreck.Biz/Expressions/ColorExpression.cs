using EdhWreck.Biz.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class ColorExpression(ValueOperator oper, string value) : KeyValueExpression("c", oper, value)
    {
        public ColorExpression(ValueOperator oper, Colors colors) : this(oper, colors.ToQueryString()) { }

        public ColorExpression(ValueOperator oper, int count) : this(oper, count.ToString()) { }
    }
}
