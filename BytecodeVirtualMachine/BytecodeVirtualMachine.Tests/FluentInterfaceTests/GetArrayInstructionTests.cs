using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class GetArrayInstructionTests
    {
        private byte _id;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _id = 1;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for id
                (byte)InstructionsEnum.Literal,
                _id,

                //define new type
                (byte)InstructionsEnum.GetArray
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new GetArrayInstruction()
                .Id(_id)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new GetArrayInstruction()
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
                (byte)InstructionsEnum.GetArray
            };

            List<byte> actual = new GetArrayInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
