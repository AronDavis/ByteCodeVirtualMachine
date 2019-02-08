using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByteCodeVirtualMachine
{
    internal class AdditionExpression : IExpression<int>
    {
        IExpression<int> _left;
        IExpression<int> _right;
        public AdditionExpression(IExpression<int> left, IExpression<int> right)
        {
            _left = left;
            _right = right;
        }

        public int Evaluate()
        {
            return _left.Evaluate() + _right.Evaluate();
        }

    }
}
