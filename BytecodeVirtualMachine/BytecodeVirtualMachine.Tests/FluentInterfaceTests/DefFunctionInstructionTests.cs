using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class DefFunctionInstructionTests
    {
        private byte _functionId;
        private byte _typeId;
        private byte _val;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _functionId = 1;
            _typeId = 2;
            _val = 3;

            _expected = new LiteralInstruction(_functionId).ToInstructions();

            _expected.Add((byte)InstructionsEnum.DefFunction);

            new ReturnSignatureInstruction(_typeId).ToInstructions(_expected);

            new LiteralInstruction(_val).ToInstructions(_expected);

            _expected.Add((byte)InstructionsEnum.EndDefFunction);
        }

        [TestMethod]
        public void Test()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefFunctionInstruction()
                .Id(_functionId)
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
