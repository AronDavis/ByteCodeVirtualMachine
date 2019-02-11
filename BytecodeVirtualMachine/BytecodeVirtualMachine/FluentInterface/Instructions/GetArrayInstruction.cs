using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class GetArrayInstruction : IInstruction
    {
        private LiteralInstruction _id;
        
        public GetArrayInstruction Id(LiteralInstruction id)
        {
            _id = id;
            return this;
        }

        public GetArrayInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public void ToInstructions(List<byte> instructions)
        {
            _id.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.GetArray);
        }
    }
}
