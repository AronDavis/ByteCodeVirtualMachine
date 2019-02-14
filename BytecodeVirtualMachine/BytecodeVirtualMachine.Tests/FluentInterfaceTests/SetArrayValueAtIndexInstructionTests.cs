using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class SetArrayValueAtIndexInstructionTests
    {
        private byte _val1;
        private byte _val2;
        private byte _id;
        private byte _index;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val1 = 1;
            _val2 = 2;
            _id = 3;
            _index = 4;
            
            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for value
                (byte)InstructionsEnum.Literal,
                _val1,

                //set literal for value
                (byte)InstructionsEnum.Literal,
                _val2,

                //set literal for array id
                (byte)InstructionsEnum.Literal,
                _id,

                //set literal for index
                (byte)InstructionsEnum.Literal,
                _index,

                (byte)InstructionsEnum.SetArrayValueAtIndex
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new SetArrayValueAtIndexInstruction()
                .Value(_val1)
                .Value(_val2)
                .Id(_id)
                .Index(_index)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();
            
            List<byte> actual = new SetArrayValueAtIndexInstruction()
                .Value(new LiteralInstruction(_val1))
                .Value(new LiteralInstruction(_val2))
                .Id(new LiteralInstruction(_id))
                .Index(new LiteralInstruction(_index))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
