using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class InstructionAggregate: IInstruction
    {
        List<IInstruction> _instructions = new List<IInstruction>();
        public void ToInstructions(List<byte> instructions)
        {
            foreach (var instruction in _instructions)
                instruction.ToInstructions(instructions);
        }

        public MathInstruction Add()
        {
            MathInstruction instruction = new MathInstruction(InstructionsEnum.Add);
            _instructions.Add(instruction);
            return instruction;
        }

        public MathInstruction Subtract()
        {
            MathInstruction instruction = new MathInstruction(InstructionsEnum.Subtract);
            _instructions.Add(instruction);
            return instruction;
        }

        public MathInstruction Multiply()
        {
            MathInstruction instruction = new MathInstruction(InstructionsEnum.Multiply);
            _instructions.Add(instruction);
            return instruction;
        }

        public MathInstruction Divide()
        {
            MathInstruction instruction = new MathInstruction(InstructionsEnum.Divide);
            _instructions.Add(instruction);
            return instruction;
        }

        public DefTypeInstruction DefType()
        {
            DefTypeInstruction instruction = new DefTypeInstruction();
            _instructions.Add(instruction);
            return instruction;
        }

        public DefArrayInstruction DefArray()
        {
            DefArrayInstruction instruction = new DefArrayInstruction();
            _instructions.Add(instruction);
            return instruction;
        }

        public SetArrayValueAtIndexInstruction SetArrayValueAtIndex()
        {
            SetArrayValueAtIndexInstruction instruction = new SetArrayValueAtIndexInstruction();
            _instructions.Add(instruction);
            return instruction;
        }

        public GetArrayInstruction GetArray()
        {
            GetArrayInstruction instruction = new GetArrayInstruction();
            _instructions.Add(instruction);
            return instruction;
        }

        public ReturnInstruction Return()
        {
            ReturnInstruction instruction = new ReturnInstruction();
            _instructions.Add(instruction);
            return instruction;
        }
    }
}
