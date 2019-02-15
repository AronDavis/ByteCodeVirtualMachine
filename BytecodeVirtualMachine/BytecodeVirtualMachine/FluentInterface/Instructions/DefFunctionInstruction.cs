using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class DefFunctionInstruction : InstructionBase
    {
        private IInstruction _id;
        private ReturnSignatureInstruction _returnSignature;
        private InstructionAggregate _body;

        public DefFunctionInstruction Id(byte id)
        {
            _id = new LiteralInstruction(id);
            return this;
        }

        public DefFunctionInstruction Id(IInstruction id)
        {
            _id = id;
            return this;
        }

        public DefFunctionInstruction ReturnSignature(byte typeId, bool isArray = false)
        {
            _returnSignature = new ReturnSignatureInstruction()
                .Type(typeId)
                .IsArray(isArray);
            return this;
        }

        public DefFunctionInstruction Body(Action<InstructionAggregate> makeBody)
        {
            InstructionAggregate body = new InstructionAggregate();
            makeBody(body);
            _body = body;
            return this;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            _id?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.DefFunction);
            _returnSignature?.ToInstructions(instructions);
            _body?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.EndDefFunction);
        }
    }
}
