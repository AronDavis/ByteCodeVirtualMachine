using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ByteCodeVirtualMachine.Tests
{
    [TestClass]
    public class IfTests
    {
        [TestMethod]
        public void IfTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 1 so the if results in true
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.If,

                //set literal to 7 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                7,

                //end if
                (byte)InstructionsEnum.EndIf,

                //add 3 + 7
                (byte)InstructionsEnum.Add
            };

            //confirm 3 + 7
            vm.Interpret(data);
            Assert.AreEqual(10, vm.Peek());
        }

        [TestMethod]
        public void IfFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 3 so we can confirm later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 2 so we can confirm later
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 0 so the if results in false
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.If,

                //attempt to set literal to 7, but shouldn't happen because if statement resulted in false
                (byte)InstructionsEnum.Literal,
                7,

                (byte)InstructionsEnum.EndIf,

                //add 3 + 2
                (byte)InstructionsEnum.Add,
            };

            //confirm that 3, not 7, is on top of the stack
            vm.Interpret(data);
            Assert.AreEqual(5, vm.Peek());
        }

        [TestMethod]
        public void NestedIfBothTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 3 so we can confirm later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 1 so the if results in true
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.If,

                //set literal to 1 so the second if results in true
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.If,

                //set literal to 7 so we can test that it stays at the top of the stack
                (byte)InstructionsEnum.Literal,
                7,

                //end both ifs
                (byte)InstructionsEnum.EndIf,
                (byte)InstructionsEnum.EndIf,

                (byte)InstructionsEnum.Add,
            };
            //confirm that 7 is on top of the stack
            vm.Interpret(data);
            Assert.AreEqual(10, vm.Peek());
        }

        [TestMethod]
        public void NestedIfBothFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 3 so we can confirm later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 2 so we can confirm later
                (byte)InstructionsEnum.Literal,
                2,

                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.If,

                //set literal to 0 so the if results in false
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.If,

                (byte)InstructionsEnum.Literal,
                7,

                (byte)InstructionsEnum.EndIf,
                (byte)InstructionsEnum.EndIf,

                //add 3 + 2
                (byte)InstructionsEnum.Add,
            };
            vm.Interpret(data);
            Assert.AreEqual(5, vm.Peek());
        }

        [TestMethod]
        public void NestedIfTrueThenFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 1 so the if results in true
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.If,

                //set literal to 3 so we can check the value later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 0 so the if results in false
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.If,

                //attempt to set literal to 7 so we can confirm it doesn't happen
                (byte)InstructionsEnum.Literal,
                7,

                //end if
                (byte)InstructionsEnum.EndIf,

                //set literal to 5 so we can confirm instructions happen before and after 2nd if
                (byte)InstructionsEnum.Literal,
                5,

                //add 3 and 5 to confirm
                (byte)InstructionsEnum.Add, 

                //end if
                (byte)InstructionsEnum.EndIf,
            };
            //confirm 3 + 5
            vm.Interpret(data);
            Assert.AreEqual(8, vm.Peek());
        }

        [TestMethod]
        public void NestedIfFalseThenTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set literal to 2 so we can confirm later
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 0 so the if results in false
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.If,

                //attempt to set literal to 13, but won't because false
                (byte)InstructionsEnum.Literal,
                13,

                //set literal to 1 so the if results in true (but skipped because nested in false)
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.If,

                //attempt to set literal to 7, but won't because false
                (byte)InstructionsEnum.Literal,
                7,

                //end inside if
                (byte)InstructionsEnum.EndIf,

                //attempt to set literal to 19, but won't because false
                (byte)InstructionsEnum.Literal,
                19,

                (byte)InstructionsEnum.EndIf,

                //set literal to 3 so we can confirm later
                (byte)InstructionsEnum.Literal,
                3,

                //add 2 + 3
                (byte)InstructionsEnum.Add,
            };

            //confirm 2 + 3
            vm.Interpret(data);
            Assert.AreEqual(5, vm.Peek());
        }
    }
}
