using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class LiteralInstruction : InstructionBase
    {
        byte _val;
        public LiteralInstruction(byte val)
        {
            _val = val;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            instructions.Add((byte)InstructionsEnum.Literal);
            instructions.Add(_val);
        }
    }
}
