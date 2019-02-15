using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class ForInstructionTests
    {
        private byte _numberOfLoops;
        private byte _val;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _numberOfLoops = 3;
            _val = 1;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for number of loops 
                (byte)InstructionsEnum.Literal,
                _numberOfLoops,

                (byte)InstructionsEnum.For,

                //set literal for body contents
                (byte)InstructionsEnum.Literal,
                _val,

                //end for
                (byte)InstructionsEnum.EndFor
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ForInstruction()
                .NumberOfLoops(_numberOfLoops)
                .Body(b =>
                {
                    b.Literal(_val);
                })
                .ToInstructions();
            
            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestExpressions()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new ForInstruction()
                .NumberOfLoops(new LiteralInstruction(_numberOfLoops))
                .Body(b =>
                {
                    b.Literal(_val);
                })
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoBodyOrNumberOfLoops()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.For,
                (byte)InstructionsEnum.EndFor
            };

            List<byte> actual = new ForInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
