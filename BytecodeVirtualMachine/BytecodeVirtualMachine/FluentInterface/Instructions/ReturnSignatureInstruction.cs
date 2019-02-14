using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class ReturnSignatureInstruction : InstructionBase
    {
        private byte _typeId;
        private bool _isArray;

        public ReturnSignatureInstruction(byte typeId, bool isArray = false)
        {
            _typeId = typeId;
            _isArray = isArray;
        }

        public override void ToInstructions(List<byte> instructions)
        {
            if (_typeId == 0)
            {
                //set return type to null
                instructions.Add((byte)InstructionsEnum.Literal);
                instructions.Add(_typeId);
                instructions.Add((byte)InstructionsEnum.ReturnSignature);

                return;
            }

            //set literal to define if return type is an array
            instructions.Add((byte)InstructionsEnum.Literal);
            instructions.Add((byte)(_isArray ? 1 : 0));

            //set literal to define the return type
            instructions.Add((byte)InstructionsEnum.Literal);
            instructions.Add(_typeId);
            instructions.Add((byte)InstructionsEnum.ReturnSignature);
        }
    }
}
