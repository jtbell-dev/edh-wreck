using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class OracleTextExpression(string value) : KeyValueExpression("o", ValueOperator.Default, $"\"{value.Trim('\"')}\"")
    {
    }
}
