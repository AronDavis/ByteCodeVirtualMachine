using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class ReturnSignatureInstruction : InstructionBase
    {
        private IInstruction _type;
        private IInstruction _isArray;

        public ReturnSignatureInstruction Type(IInstruction type)
        {
            _type = type;
            return this;
        }

        public ReturnSignatureInstruction Type(byte type)
        {
            _type = new LiteralInstruction(type);
            return this;
        }

        public ReturnSignatureInstruction IsArray(IInstruction isArray)
        {
            _isArray = isArray;
            return this;
        }

        public ReturnSignatureInstruction IsArray(byte isArray)
        {
            _isArray = new LiteralInstruction(isArray);
            return this;
        }

        public ReturnSignatureInstruction IsArray(bool isArray)
        {
            return IsArray((byte)(isArray ? 1 : 0));
        }
        
        public override void ToInstructions(List<byte> instructions)
        {
            _isArray?.ToInstructions(instructions);
            _type?.ToInstructions(instructions);
            instructions.Add((byte)InstructionsEnum.ReturnSignature);
        }
    }
}
