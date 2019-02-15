using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class DefVarInstructionTests
    {
        private byte _type;
        private byte _id;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _type = 1;
            _id = 2;
            
            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for type
                (byte)InstructionsEnum.Literal,
                _type,

                //set literal for array id
                (byte)InstructionsEnum.Literal,
                _id,

                (byte)InstructionsEnum.DefVar
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefVarInstruction()
                .Type(_type)
                .Id(_id)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();
            
            List<byte> actual = new DefVarInstruction()
                .Type(new LiteralInstruction(_type))
                .Id(new LiteralInstruction(_id))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoIdOrType()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.DefVar
            };

            List<byte> actual = new DefVarInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
