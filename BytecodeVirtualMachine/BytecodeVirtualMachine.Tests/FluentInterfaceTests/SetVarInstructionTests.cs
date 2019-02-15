using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class SetVarInstructionTests
    {
        private byte _val;
        private byte _id;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 1;
            _id = 3;
            
            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for value
                (byte)InstructionsEnum.Literal,
                _val,

                //set literal for array id
                (byte)InstructionsEnum.Literal,
                _id,

                (byte)InstructionsEnum.SetVar
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new SetVarInstruction()
                .Value(_val)
                .Id(_id)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();
            
            List<byte> actual = new SetVarInstruction()
                .Value(new LiteralInstruction(_val))
                .Id(new LiteralInstruction(_id))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoIdOrValue()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.SetVar
            };

            List<byte> actual = new SetVarInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
