using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class GetVarInstructionTests
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
                (byte)InstructionsEnum.GetVar
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new GetVarInstruction()
                .Id(_id)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new GetVarInstruction()
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
                (byte)InstructionsEnum.GetVar
            };

            List<byte> actual = new GetVarInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
