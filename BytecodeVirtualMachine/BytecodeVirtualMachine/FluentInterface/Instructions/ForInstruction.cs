using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class ForInstruction : InstructionBase
    {
        private IInstruction _numberOfLoops;
        private InstructionAggregate _body;

        public ForInstruction NumberOfLoops(IInstruction numberOfLoops)
        {
            _numberOfLoops = numberOfLoops;
            return this;
        }

        public ForInstruction NumberOfLoops(byte numberOfLoops)
        {
            _numberOfLoops = new LiteralInstruction(numberOfLoops);
            return this;
        }

        public ForInstruction Body(Action<InstructionAggregate> makeBody)
        {
            InstructionAggregate body = new InstructionAggregate();
            makeBody(body);
            _body = body;
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _numberOfLoops?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.For);
            _body.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.EndFor);
        }
    }
}
