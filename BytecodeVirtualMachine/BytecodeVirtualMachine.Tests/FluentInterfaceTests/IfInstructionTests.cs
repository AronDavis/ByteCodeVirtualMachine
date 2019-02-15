using BytecodeVirtualMachine.FluentInterface.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.FluentInterfaceTests
{
    [TestClass]
    public class IfInstructionTests
    {
        private byte _val;
        private bool _condition;

        private List<byte> _expected;
        
        [TestInitialize]
        public void Init()
        {
            _val = 1;
            _condition = true;

            _expected = new List<byte>();

            _expected.AddRange<byte>(
                //set literal for number of loops 
                (byte)InstructionsEnum.Literal,
                (byte)(_condition ? 1 : 0),

                (byte)InstructionsEnum.If,

                //set literal for body contents
                (byte)InstructionsEnum.Literal,
                _val,

                //end for
                (byte)InstructionsEnum.EndIf
            );
        }

        [TestMethod]
        public void TestValues()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> actual = new IfInstruction()
                .Condition(_condition)
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

            List<byte> actual = new IfInstruction()
                .Condition(new LiteralInstruction((byte)(_condition ? 1 : 0)))
                .Body(b =>
                {
                    b.Literal(_val);
                })
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }

        [TestMethod]
        public void TestNoConditionOrBody()
        {
            VirtualMachine vm = new VirtualMachine();

            _expected = new List<byte>()
            {
                (byte)InstructionsEnum.If,
                (byte)InstructionsEnum.EndIf
            };

            List<byte> actual = new IfInstruction()
                .ToInstructions();

            TestHelper.AssertResultsEqual(_expected, actual);
        }
    }
}
