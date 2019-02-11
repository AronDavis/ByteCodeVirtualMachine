using System.Collections.Generic;

namespace BytecodeVirtualMachine
{
    internal class BytecodeFunction
    {
        public List<byte> Instructions;
        public byte ReturnType;
        public bool ShouldReturnArray;

        public BytecodeFunction(IList<byte> instructions)
        {
            Instructions = new List<byte>(instructions);
        }

        public BytecodeFunction()
        {
            Instructions = new List<byte>();
        }
    }
}
