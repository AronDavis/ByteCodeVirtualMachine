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

        private T _getInstruction<T>() where T : IInstruction, new()
        {
            T instruction = new T();
            _instructions.Add(instruction);
            return instruction;
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

        public DefTypeInstruction DefType() => _getInstruction<DefTypeInstruction>();

        public ForInstruction For() => _getInstruction<ForInstruction>();

        public DefArrayInstruction DefArray() => _getInstruction<DefArrayInstruction>();

        public SetArrayValueAtIndexInstruction SetArrayValueAtIndex() => _getInstruction<SetArrayValueAtIndexInstruction>();

        public GetArrayInstruction GetArray() => _getInstruction<GetArrayInstruction>();

        public DefVarInstruction DefVar() => _getInstruction<DefVarInstruction>();

        public GetVarInstruction GetVar() => _getInstruction<GetVarInstruction>();

        public SetVarInstruction SetVar() => _getInstruction<SetVarInstruction>();

        public CustomFunctionInstruction CustomFunction() => _getInstruction<CustomFunctionInstruction>();

        public ReturnInstruction Return() => _getInstruction<ReturnInstruction>();
    }
}
