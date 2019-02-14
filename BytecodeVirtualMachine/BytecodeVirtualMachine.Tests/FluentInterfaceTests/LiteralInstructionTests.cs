using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class LiteralInstructionTests
    {
        private byte _val;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 1;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal
                (byte)InstructionsEnum.Literal,
                _val
            );
        }

        [TestMethod]
        public void TestValue()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new LiteralInstruction(_val)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
