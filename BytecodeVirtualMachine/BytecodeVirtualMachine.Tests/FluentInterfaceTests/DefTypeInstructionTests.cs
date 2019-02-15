using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class DefTypeInstructionTests
    {
        private byte _id;
        private byte _numberOfFields;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _id = 1;
            _numberOfFields = 2;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for id
                (byte)InstructionsEnum.Literal,
                _id,

                //set literal for number of fields
                (byte)InstructionsEnum.Literal,
                _numberOfFields,

                //define new type
                (byte)InstructionsEnum.DefType
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefTypeInstruction()
                .Id(_id)
                .NumberOfFields(_numberOfFields)
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new DefTypeInstruction()
                .Id(new LiteralInstruction(_id))
                .NumberOfFields(new LiteralInstruction(_numberOfFields))
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoIdOrNumberOfFields()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.DefType
            };

            List<byte> actual = new DefTypeInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
