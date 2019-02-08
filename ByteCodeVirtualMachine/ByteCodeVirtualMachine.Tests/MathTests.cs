using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Addition()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set signature to return one byte
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.ReturnSignature,

                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 5 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                5,
                
                //add 3 + 5
                (byte)InstructionsEnum.Add,

                //return
                (byte)InstructionsEnum.Return
            };

            var results = vm.Interpret(data);

            //confirm 3 + 5
            Assert.AreEqual(8, results[0]);
        }

        [TestMethod]
        public void Subtraction()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set signature to return one byte
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.ReturnSignature,

                //set literal to 5 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                5,

                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                3,
                
                //subtract 5-3
                (byte)InstructionsEnum.Subtract,

                //return
                (byte)InstructionsEnum.Return
            };

            var results = vm.Interpret(data);

            //confirm 3 + 5
            Assert.AreEqual(2, results[0]);
        }

        [TestMethod]
        public void Multiplication()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set signature to return one byte
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.ReturnSignature,

                //set literal to 5 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                5,

                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                3,
                
                //multiply 5 * 3
                (byte)InstructionsEnum.Multiply,

                //return
                (byte)InstructionsEnum.Return
            };

            var results = vm.Interpret(data);

            //confirm 5 * 3
            Assert.AreEqual(15, results[0]);
        }

        [TestMethod]
        public void Division()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                //set signature to return one byte
                (byte)InstructionsEnum.Literal,
                1,
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.ReturnSignature,

                //set literal to 5 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                6,

                //set literal to 3 so we can confirm value later
                (byte)InstructionsEnum.Literal,
                2,
                
                //divide 6/2
                (byte)InstructionsEnum.Divide,

                //return
                (byte)InstructionsEnum.Return
            };

            var results = vm.Interpret(data);

            //confirm 6/2
            Assert.AreEqual(3, results[0]);
        }
    }
}
