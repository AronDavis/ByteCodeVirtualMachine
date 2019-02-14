using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class ReturnInstructionTests
    {
        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _expected = new List<byte>();

            _expected.AddRange<byte>(
                (byte)InstructionsEnum.Return
            );
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ReturnInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
