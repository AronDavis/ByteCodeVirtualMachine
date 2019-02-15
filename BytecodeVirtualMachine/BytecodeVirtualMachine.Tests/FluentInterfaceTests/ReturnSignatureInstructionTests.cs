using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class ReturnSignatureInstructionTests
    {
        private byte _isArray;
        private byte _type;
        private List<byte> _expected;

        [TestInitialize]
        public void Init()
        {
            _isArray = 1;
            _type = 2;
            _expected = new List<byte>()
            {
                //set literal for array
                (byte)InstructionsEnum.Literal,
                _isArray,

                //set literal for return type
                (byte)InstructionsEnum.Literal,
                _type,
                (byte)InstructionsEnum.ReturnSignature
            };
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();
            List<byte> actual = new ReturnSignatureInstruction()
                .Type(_type)
                .IsArray(_isArray)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestBooleanValue()
        {
            VirtualMachine vm = new VirtualMachine();
            List<byte> actual = new ReturnSignatureInstruction()
                .Type(_type)
                .IsArray(_isArray == 1)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();
            List<byte> actual = new ReturnSignatureInstruction()
                .Type(new LiteralInstruction(_type))
                .IsArray(new LiteralInstruction(_isArray))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoTypeOrIsArray()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.ReturnSignature
            };

            List<byte> actual = new ReturnSignatureInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
