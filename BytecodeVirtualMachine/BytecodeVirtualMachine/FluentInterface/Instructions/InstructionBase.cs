using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public abstract class InstructionBase : IInstruction
    {
        public abstract void ToInstructions(List<byte> instructions);

        public List<byte> ToInstructions()
        {
            List<byte> instructions = new List<byte>();
            ToInstructions(instructions);
            return instructions;
        }
    }
}
