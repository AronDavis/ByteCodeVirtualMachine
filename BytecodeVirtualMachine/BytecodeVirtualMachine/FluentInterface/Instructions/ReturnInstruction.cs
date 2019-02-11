using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class ReturnInstruction : IInstruction
    {
        public void ToInstructions(List<byte> instructions)
        {
            instructions.Add((byte)InstructionsEnum.Return);
        }
    }
}
