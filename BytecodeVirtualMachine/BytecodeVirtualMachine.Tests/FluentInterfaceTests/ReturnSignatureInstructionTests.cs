using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class ReturnSignatureInstructionTests
    {
        [TestMethod]
        public void TestNoReturnValue()
        {
            byte typeId = 0;
            List<byte> _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for return type
                (byte)InstructionsEnum.Literal,
                typeId,
                (byte)InstructionsEnum.ReturnSignature
            );

            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ReturnSignatureInstruction(typeId)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestReturnValue()
        {
            byte typeId = 1;
            List<byte> _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for no array
                (byte)InstructionsEnum.Literal,
                0,

                //set literal for return type
                (byte)InstructionsEnum.Literal,
                typeId,
                (byte)InstructionsEnum.ReturnSignature
            );

            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ReturnSignatureInstruction(typeId)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestArrayReturnValue()
        {
            byte typeId = 1;
            bool isArray = true;
            List<byte> _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for array
                (byte)InstructionsEnum.Literal,
                1,

                //set literal for return type
                (byte)InstructionsEnum.Literal,
                typeId,
                (byte)InstructionsEnum.ReturnSignature
            );

            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ReturnSignatureInstruction(typeId, isArray)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
