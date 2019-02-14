using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class BinaryOperatorInstructionTests
    {
        private byte _val1;
        private byte _val2;
        private InstructionsEnum _binaryOperator;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val1 = 3;
            _val2 = 5;

            _binaryOperator = InstructionsEnum.Add;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                _val1,

                //set literal to 5 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                _val2,

                //add 3 + 5
                (byte)_binaryOperator
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new BinaryOperatorInstruction(_binaryOperator)
                .Left(_val1)
                .Right(_val2)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();
            
            List<byte> actual = new BinaryOperatorInstruction(_binaryOperator)
                .Left(new LiteralInstruction(_val1))
                .Right(new LiteralInstruction(_val2))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoLeftOrRight()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new LiteralInstruction(_val1).ToInstructions();

            new LiteralInstruction(_val2)
                .ToInstructions(actual);

            new BinaryOperatorInstruction(_binaryOperator)
                .ToInstructions(actual);

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
