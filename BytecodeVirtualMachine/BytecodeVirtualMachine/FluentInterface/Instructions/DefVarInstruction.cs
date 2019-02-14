using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class DefVarInstruction : InstructionBase
    {
        private IInstruction _type;
        private IInstruction _id;

        public DefVarInstruction Type(IInstruction type)
        {
            _type = type;
            return this;
        }

        public DefVarInstruction Type(byte type)
        {
            _type = new LiteralInstruction(type);
            return this;
        }

        public DefVarInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public DefVarInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }
        
        public override void ToInstructions(List<byte> instructions)
        {
            _type?.ToInstructions(instructions);
            _id?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.DefVar);
        }
    }
}
