using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class DefArrayInstructionTests
    {
        private byte _type;
        private byte _id;
        private byte _length;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _type = 1;
            _id = 2;
            _length = 3;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for type
                (byte)InstructionsEnum.Literal,
                _type,

                //set literal for array id
                (byte)InstructionsEnum.Literal,
                _id,

                //set literal for length
                (byte)InstructionsEnum.Literal,
                _length,

                //define type_2[] array_0 = new type_2[]
                (byte)InstructionsEnum.DefArray
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefArrayInstruction()
                .Type(_type)
                .Id(_id)
                .Length(_length)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {

            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefArrayInstruction()
                .Type(new LiteralInstruction(_type))
                .Id(new LiteralInstruction(_id))
                .Length(new LiteralInstruction(_length))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
