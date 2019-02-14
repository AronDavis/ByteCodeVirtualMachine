using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class ReturnInstruction : InstructionBase
    {
        public override void ToInstructions(List<byte> instructions)
        {
            instructions.Add((byte)InstructionsEnum.Return);
        }
    }
}
