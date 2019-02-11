using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class DefFunctionInstruction : IInstruction
    {
        private ReturnSignatureInstruction _returnSignature;
        private IInstruction _body;

        public DefFunctionInstruction ReturnSignature(byte typeId, bool isArray = false)
        {
            _returnSignature = new ReturnSignatureInstruction(typeId, isArray);
            return this;
        }

        public DefFunctionInstruction Body(Action<InstructionAggregate> makeBody)
        {
            InstructionAggregate body = new InstructionAggregate();
            makeBody(body);
            _body = body;
            return this;
        }

        public void ToInstructions(List<byte> instructions)
        {
            _returnSignature.ToInstructions(instructions);
            _body.ToInstructions(instructions);
        }
    }
}
