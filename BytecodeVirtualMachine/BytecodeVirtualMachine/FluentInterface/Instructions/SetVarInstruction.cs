using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class SetVarInstruction : InstructionBase
    {
        private IInstruction _val;
        private IInstruction _id;

        public SetVarInstruction Value(IInstruction val)
        {
            _val = val;
            return this;
        }

        public SetVarInstruction Value(byte val)
        {
            _val = new LiteralInstruction(val);
            return this;
        }

        public SetVarInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public SetVarInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }
        
        public override void ToInstructions(List<byte> instructions)
        {
            _val?.ToInstructions(instructions);
            _id?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.SetVar);
        }
    }
}
