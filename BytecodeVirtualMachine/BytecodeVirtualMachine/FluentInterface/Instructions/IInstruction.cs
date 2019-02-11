using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public interface IInstruction
    {
        void ToInstructions(List<byte> instructions);
    }
}
