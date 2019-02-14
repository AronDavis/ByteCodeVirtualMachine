using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class GetVarInstruction : InstructionBase
    {
        private IInstruction _id;
        
        public GetVarInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public GetVarInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _id?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.GetVar);
        }
    }
}
