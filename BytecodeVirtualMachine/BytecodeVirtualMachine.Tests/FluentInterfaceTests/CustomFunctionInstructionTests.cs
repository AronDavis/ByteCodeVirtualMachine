using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class CustomFunctionInstructionTests
    {
        private byte _id;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _id = 1;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal
                (byte)InstructionsEnum.Literal,
                _id,
                (byte)InstructionsEnum.CustomFunction
            );
        }

        [TestMethod]
        public void TestValue()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new CustomFunctionInstruction()
                .Id(_id)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpression()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new CustomFunctionInstruction()
                .Id(new LiteralInstruction(_id))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoId()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.CustomFunction
            };

            List<byte> actual = new CustomFunctionInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
