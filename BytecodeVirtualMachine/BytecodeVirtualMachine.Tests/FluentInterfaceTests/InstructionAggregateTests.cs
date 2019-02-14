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

            new BinaryOperatorInstruction(InstructionsEnum.Add).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Subtract).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Multiply).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.Divide).Left(_val).Right(_val).ToInstructions(_expected);

            new BinaryOperatorInstruction(InstructionsEnum.EqualTo).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.GreaterThanOrEqualTo).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.GreaterThan).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.LessThanOrEqualTo).Left(_val).Right(_val).ToInstructions(_expected);
            new BinaryOperatorInstruction(InstructionsEnum.LessThan).Left(_val).Right(_val).ToInstructions(_expected);

            new IfInstruction().Condition(_condition).ToInstructions(_expected);

            new DefTypeInstruction().Id(_val).NumberOfFields(_val).ToInstructions(_expected);

            new ForInstruction().NumberOfLoops(_val).ToInstructions(_expected);

            new DefArrayInstruction().Id(_val).Length(_val).Type(_val).ToInstructions(_expected);
            new SetArrayValueAtIndexInstruction().Id(_val).Index(_val).Value(_val).ToInstructions(_expected);
            new GetArrayInstruction().Id(_val).ToInstructions(_expected);

            new DefVarInstruction().Id(_val).Type(_val).ToInstructions(_expected);
            new GetVarInstruction().Id(_val).ToInstructions(_expected);
            new SetVarInstruction().Id(_val).Value(_val).ToInstructions(_expected);

            new CustomFunctionInstruction().Id(_val).ToInstructions(_expected);

            new ReturnInstruction().ToInstructions(_expected);
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            var instructionAggregate = new InstructionAggregate();

            instructionAggregate.Literal(_val);

            instructionAggregate.Add()
                .Left(_val)
                .Right(_val);

            instructionAggregate.Subtract()
                .Left(_val)
                .Right(_val);

            instructionAggregate.Multiply()
                .Left(_val)
                .Right(_val);

            instructionAggregate.Divide()
                .Left(_val)
                .Right(_val);

            instructionAggregate.EqualTo()
                .Left(_val)
                .Right(_val);

            instructionAggregate.GreaterThanOrEqualTo()
                .Left(_val)
                .Right(_val);

            instructionAggregate.GreaterThan()
                .Left(_val)
                .Right(_val);

            instructionAggregate.LessThanOrEqualTo()
                .Left(_val)
                .Right(_val);

            instructionAggregate.LessThan()
                .Left(_val)
                .Right(_val);

            instructionAggregate.If()
                .Condition(_condition);

            instructionAggregate.DefType()
                .Id(_val)
                .NumberOfFields(_val);

            instructionAggregate.For()
                .NumberOfLoops(_val);

            instructionAggregate.DefArray()
                .Id(_val)
                .Length(_val)
                .Type(_val);

            instructionAggregate.SetArrayValueAtIndex()
                .Id(_val)
                .Index(_val)
                .Value(_val);

            instructionAggregate.GetArray()
                .Id(_val);

            instructionAggregate.DefVar()
                .Id(_val)
                .Type(_val);

            instructionAggregate.GetVar()
                .Id(_val);

            instructionAggregate.SetVar()
                .Id(_val)
                .Value(_val);

            instructionAggregate.CustomFunction()
                .Id(_val);

            instructionAggregate.Return();

            List<byte> actual = instructionAggregate.ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
