using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class GetArrayInstruction : InstructionBase
    {
        private IInstruction _id;
        
        public GetArrayInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public GetArrayInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _id.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.GetArray);
        }
    }
}
