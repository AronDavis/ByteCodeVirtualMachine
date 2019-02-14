using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class SetArrayValueAtIndexInstruction : InstructionBase
    {
        private List<IInstruction> _values = new List<IInstruction>();
        private IInstruction _id;
        private IInstruction _index;

        public SetArrayValueAtIndexInstruction Value(IInstruction value)
        {
            _values.Add(value);
            return this;
        }

        public SetArrayValueAtIndexInstruction Value(byte value)
        {
            _values.Add(new LiteralInstruction(value));
            return this;
        }

        public SetArrayValueAtIndexInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public SetArrayValueAtIndexInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public SetArrayValueAtIndexInstruction Index(IInstruction index)
        {
            _index = index;
            return this;
        }
        public SetArrayValueAtIndexInstruction Index(byte index)
        {
            _index = new LiteralInstruction(index);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            foreach (var value in _values)
                value.ToInstructions(instructions);

            _id.ToInstructions(instructions);
            _index.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.SetArrayValueAtIndex);
        }
    }
}
