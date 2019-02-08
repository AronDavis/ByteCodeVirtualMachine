using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class ForTests
    {
        [TestMethod]
        public void For()
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

                //set literal to 0 so we have a start value to add
                (byte)InstructionsEnum.Literal,
                0,

                //set literal to 5 so we loop 5 times
                (byte)InstructionsEnum.Literal,
                5,
                (byte)InstructionsEnum.For,

                //set literal to 2 so we can add
                (byte)InstructionsEnum.Literal,
                2,

                //add 0 + 2 or 2 + 2
                (byte)InstructionsEnum.Add,

                //end for
                (byte)InstructionsEnum.EndFor,

                //return
                (byte)InstructionsEnum.Return
            };

            //confirm that five adds happened: 0 + 2 + 2 + 2 + 2 + 2
            var results = vm.Interpret(data);
            Assert.AreEqual(10, results[0]);
        }

        [TestMethod]
        public void NestedFor()
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

                //set literal to 0 so we have a start value to add
                (byte)InstructionsEnum.Literal,
                0,

                //set literal to 2 so we loop 2 times
                (byte)InstructionsEnum.Literal,
                2,
                (byte)InstructionsEnum.For,

                //set literal to 5 so we can add
                (byte)InstructionsEnum.Literal,
                5,

                //add
                (byte)InstructionsEnum.Add,

                //set literal to 10 so we loop 10 times
                (byte)InstructionsEnum.Literal,
                10,
                (byte)InstructionsEnum.For,

                //set literal to 3 so we can add
                (byte)InstructionsEnum.Literal,
                3,

                //add
                (byte)InstructionsEnum.Add,

                //end interal for
                (byte)InstructionsEnum.EndFor,

                //end external for
                (byte)InstructionsEnum.EndFor,

                //return
                (byte)InstructionsEnum.Return
            };

            //confirm [(5) + (30)] + [(5) + (30)]
            var results = vm.Interpret(data);
            Assert.AreEqual(70, results[0]);
        }
    }
}
