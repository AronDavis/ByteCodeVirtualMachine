namespace ByteCodeVirtualMachine
{
    internal class NumberExpression : IExpression<int>
    {
        private int _value;
        public NumberExpression(int value)
        {
            _value = value;
        }

        public int Evaluate()
        {
            return _value;
        }

    }
}
