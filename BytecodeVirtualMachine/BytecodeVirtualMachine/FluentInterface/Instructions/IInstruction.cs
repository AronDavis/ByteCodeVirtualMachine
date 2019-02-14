using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public interface IInstruction : IFluentInterface
    {
        void ToInstructions(List<byte> instructions);

        List<byte> ToInstructions();
    }
}
