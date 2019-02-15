using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class DefArrayInstruction : InstructionBase
    {
        private IInstruction _type;
        private IInstruction _id;
        private IInstruction _length;

        public DefArrayInstruction Type(IInstruction type)
        {
            _type = type;
            return this;
        }

        public DefArrayInstruction Type(byte type)
        {
            _type = new LiteralInstruction(type);
            return this;
        }

        public DefArrayInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public DefArrayInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public DefArrayInstruction Length(IInstruction length)
        {
            _length = length;
            return this;
        }

        public DefArrayInstruction Length(byte length)
        {
            _length = new LiteralInstruction(length);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _type?.ToInstructions(instructions);
            _id?.ToInstructions(instructions);
            _length?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.DefArray);
        }
    }
}
