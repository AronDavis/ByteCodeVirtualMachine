using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class DefTypeInstruction : InstructionBase
    {
        private IInstruction _id;
        private IInstruction _numberOfFields;

        public DefTypeInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public DefTypeInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public DefTypeInstruction NumberOfFields(IInstruction numberOfFields)
        {
            _numberOfFields = numberOfFields;
            return this;
        }

        public DefTypeInstruction NumberOfFields(byte numberOfFields)
        {
            _numberOfFields = new LiteralInstruction(numberOfFields);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _id?.ToInstructions(instructions);
            _numberOfFields?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.DefType);
        }
    }
}
