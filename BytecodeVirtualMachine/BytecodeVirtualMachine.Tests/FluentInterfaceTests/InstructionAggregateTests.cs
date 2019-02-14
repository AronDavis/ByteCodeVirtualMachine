using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class InstructionAggregateTests
    {
        private byte _val;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 0;

            _expected = new ReturnSignatureInstruction(_val).ToInstructions();

            _expected.AddRange(new LiteralInstruction(_val).ToInstructions());

            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.Add).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.Subtract).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.Multiply).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.Divide).Left(_val).Right(_val).ToInstructions());

            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.EqualTo).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.GreaterThanOrEqualTo).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.GreaterThan).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.LessThanOrEqualTo).Left(_val).Right(_val).ToInstructions());
            _expected.AddRange(new BinaryOperatorInstruction(InstructionsEnum.LessThan).Left(_val).Right(_val).ToInstructions());

            _expected.AddRange(new DefTypeInstruction().Id(_val).NumberOfFields(_val).ToInstructions());

            _expected.AddRange(new DefArrayInstruction().Id(_val).Length(_val).Type(_val).ToInstructions());

            _expected.AddRange(new SetArrayValueAtIndexInstruction().Id(_val).Index(_val).Value(_val).ToInstructions());

            _expected.AddRange(new GetArrayInstruction().Id(_val).ToInstructions());

            _expected.AddRange(new CustomFunctionInstruction().Id(_val).ToInstructions());

            _expected.AddRange(new ReturnInstruction().ToInstructions());
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefFunctionInstruction()
                .ReturnSignature(_val)
                .Body(b =>
                {
                    b.Literal(_val);

                    b.Add()
                    .Left(_val)
                    .Right(_val);

                    b.Subtract()
                    .Left(_val)
                    .Right(_val);

                    b.Multiply()
                    .Left(_val)
                    .Right(_val);

                    b.Divide()
                    .Left(_val)
                    .Right(_val);

                    b.EqualTo()
                    .Left(_val)
                    .Right(_val);

                    b.GreaterThanOrEqualTo()
                    .Left(_val)
                    .Right(_val);

                    b.GreaterThan()
                    .Left(_val)
                    .Right(_val);

                    b.LessThanOrEqualTo()
                    .Left(_val)
                    .Right(_val);

                    b.LessThan()
                    .Left(_val)
                    .Right(_val);

                    b.DefType()
                    .Id(_val)
                    .NumberOfFields(_val);

                    b.DefArray()
                    .Id(_val)
                    .Length(_val)
                    .Type(_val);

                    b.SetArrayValueAtIndex()
                    .Id(_val)
                    .Index(_val)
                    .Value(_val);

                    b.GetArray()
                    .Id(_val);

                    b.CustomFunction()
                    .Id(_val);

                    b.Return();
                })
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
