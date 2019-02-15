using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class InstructionAggregateTests
    {
        private byte _val;
        private bool _condition;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 0;
            _condition = false;

            _expected = new List<byte>();

            new LiteralInstruction(_val).ToInstructions(_expected);

            new BinaryOperatorInstruction(InstructionsEnum.Add).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Subtract).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Multiply).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Divide).ToInstructions(_expected);

            new BinaryOperatorInstruction(InstructionsEnum.EqualTo).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.GreaterThanOrEqualTo).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.GreaterThan).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.LessThanOrEqualTo).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.LessThan).ToInstructions(_expected);

            new IfInstruction().ToInstructions(_expected);

            new DefTypeInstruction().ToInstructions(_expected);

            new ForInstruction().ToInstructions(_expected);

            new DefArrayInstruction().ToInstructions(_expected);
            new SetArrayValueAtIndexInstruction().ToInstructions(_expected);
            new GetArrayInstruction().ToInstructions(_expected);

            new DefVarInstruction().ToInstructions(_expected);
            new GetVarInstruction().ToInstructions(_expected);
            new SetVarInstruction().ToInstructions(_expected);

            new CustomFunctionInstruction().ToInstructions(_expected);

            new ReturnInstruction().ToInstructions(_expected);
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            var instructionAggregate = new InstructionAggregate();

            instructionAggregate.Literal(_val);

            instructionAggregate.Add();

            instructionAggregate.Subtract();

            instructionAggregate.Multiply();

            instructionAggregate.Divide();

            instructionAggregate.EqualTo();

            instructionAggregate.GreaterThanOrEqualTo();

            instructionAggregate.GreaterThan();

            instructionAggregate.LessThanOrEqualTo();

            instructionAggregate.LessThan();

            instructionAggregate.If();

            instructionAggregate.DefType();

            instructionAggregate.For();

            instructionAggregate.DefArray();

            instructionAggregate.SetArrayValueAtIndex();

            instructionAggregate.GetArray();

            instructionAggregate.DefVar();

            instructionAggregate.GetVar();

            instructionAggregate.SetVar();

            instructionAggregate.CustomFunction();

            instructionAggregate.Return();

            List<byte> actual = instructionAggregate.ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
