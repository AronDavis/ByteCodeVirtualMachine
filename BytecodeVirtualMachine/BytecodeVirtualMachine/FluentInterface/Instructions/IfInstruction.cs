using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class IfInstruction : InstructionBase
    {
        private IInstruction _condition;
        private InstructionAggregate _body;

        public IfInstruction Condition(IInstruction condition)
        {
            _condition = condition;
            return this;
        }

        public IfInstruction Condition(bool condition)
        {
            _condition = new LiteralInstruction((byte)(condition ? 1 : 0));
            return this;
        }
        public IfInstruction Body(Action<InstructionAggregate> makeBody)
        {
            InstructionAggregate body = new InstructionAggregate();
            makeBody(body);
            _body = body;
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _condition?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.If);
            _body?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.EndIf);
        }
    }
}
