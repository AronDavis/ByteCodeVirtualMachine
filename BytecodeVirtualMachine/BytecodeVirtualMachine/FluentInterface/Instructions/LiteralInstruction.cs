using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class LiteralInstruction : IInstruction
    {
        byte _val;
        public LiteralInstruction(byte val)
        {
            _val = val;
        }

        public void ToInstructions(List<byte> instructions)
        {
            instructions.Add((byte)InstructionsEnum.Literal);
            instructions.Add(_val);
        }
    }
}
