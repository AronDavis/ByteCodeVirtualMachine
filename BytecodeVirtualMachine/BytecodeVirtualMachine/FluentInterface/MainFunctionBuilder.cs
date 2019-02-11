using BytecodeVirtualMachine.FluentInterface.Instructions;
using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface
{
    public class MainFunctionBuilder
    {
        private DefFunctionInstruction _instruction = new DefFunctionInstruction();

        public MainFunctionBuilder ReturnSignature(byte typeId, bool isArray = false)
        {
            _instruction.ReturnSignature(typeId, isArray);
            return this;
        }

        public MainFunctionBuilder Body(Action<InstructionAggregate> makeBody)
        {
            _instruction.Body(makeBody);
            return this;
        }

        public List<byte> ToInstructions()
        {
            List<byte> instructions = new List<byte>();
            _instruction.ToInstructions(instructions);
            return instructions;
        }
    }
}
