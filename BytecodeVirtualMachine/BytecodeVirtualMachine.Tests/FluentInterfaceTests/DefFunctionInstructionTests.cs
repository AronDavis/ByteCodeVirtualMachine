using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class DefFunctionInstructionTests
    {
        private byte _val;
        private byte _typeId;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 1;
            _typeId = 0;

            _expected = new ReturnSignatureInstruction(_typeId).ToInstructions();

            _expected.AddRange(new LiteralInstruction(_val).ToInstructions());
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefFunctionInstruction()
                .ReturnSignature(_typeId)
                .Body(b =>
                {
                    b.Literal(_val);
                })
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
