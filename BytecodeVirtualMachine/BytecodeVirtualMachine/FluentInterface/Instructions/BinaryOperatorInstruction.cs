using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class BinaryOperatorInstruction : InstructionBase
    {
        private InstructionsEnum _mathInstruction;
        public BinaryOperatorInstruction(InstructionsEnum mathInstruction)
        {
            _mathInstruction = mathInstruction;
        }

        private IInstruction _left;
        private IInstruction _right;
        public BinaryOperatorInstruction Left(IInstruction left)
        {
            _left = left;
            return this;
        }

        public BinaryOperatorInstruction Left(byte left)
        {
            _left = new LiteralInstruction(left);
            return this;
        }

        public BinaryOperatorInstruction Right(IInstruction right)
        {
            _right = right;
            return this;
        }

        public BinaryOperatorInstruction Right(byte right)
        {
            _right = new LiteralInstruction(right);
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _left?.ToInstructions(instructions);
            _right?.ToInstructions(instructions);
            instructions.Add((byte)_mathInstruction);
        }
    }
}
