using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class FunctionInstruction : InstructionBase
    {
        private IInstruction _id;
        
        public FunctionInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public FunctionInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _id?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.Function);
        }
    }
}
