using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByteCodeVirtualMachine
{
    internal interface IExpression<T>
    {
        T Evaluate();
    }
}
