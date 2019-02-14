using System.Collections.Generic;

namespace BytecodeVirtualMachine.FluentInterface.Instructions
{
    public class InstructionAggregate: InstructionBase
    {
        List<IInstruction> _instructions = new List<IInstruction>();
        public override void ToInstructions(List<byte> instructions)
        {
            foreach (var instruction in _instructions)
                instruction.ToInstructions(instructions);
        }

        public LiteralInstruction Literal(byte val)
        {
            LiteralInstruction instruction = new LiteralInstruction(val);
            _instructions.Add(instruction);
            return instruction;
        }

        private BinaryOperatorInstruction _binaryOperator(InstructionsEnum binaryOperator)
        {
            BinaryOperatorInstruction instruction = new BinaryOperatorInstruction(binaryOperator);
            _instructions.Add(instruction);
            return instruction;
        }
        public BinaryOperatorInstruction Add()
        {
            return _binaryOperator(InstructionsEnum.Add);
        }

        public BinaryOperatorInstruction Subtract()
        {
            return _binaryOperator(InstructionsEnum.Subtract);
        }

        public BinaryOperatorInstruction Multiply()
        {
            return _binaryOperator(InstructionsEnum.Multiply);
        }

        public BinaryOperatorInstruction Divide()
        {
            return _binaryOperator(InstructionsEnum.Divide);
        }

        public BinaryOperatorInstruction EqualTo()
        {
            return _binaryOperator(InstructionsEnum.EqualTo);
        }

        public BinaryOperatorInstruction GreaterThanOrEqualTo()
        {
            return _binaryOperator(InstructionsEnum.GreaterThanOrEqualTo);
        }

        public BinaryOperatorInstruction GreaterThan()
        {
            return _binaryOperator(InstructionsEnum.GreaterThan);
        }

        public BinaryOperatorInstruction LessThanOrEqualTo()
        {
            return _binaryOperator(InstructionsEnum.LessThanOrEqualTo);
        }

        public BinaryOperatorInstruction LessThan()
        {
            return _binaryOperator(InstructionsEnum.LessThan);
        }

        public DefTypeInstruction DefType()
        {
            DefTypeInstruction instruction = new DefTypeInstruction();
            _instructions.Add(instruction);
            return instruction;
        }

        public ForInstruction For()
        {
            ForInstruction instruction = new ForInstruction();
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

        public CustomFunctionInstruction CustomFunction()
        {
            CustomFunctionInstruction instruction = new CustomFunctionInstruction();
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
