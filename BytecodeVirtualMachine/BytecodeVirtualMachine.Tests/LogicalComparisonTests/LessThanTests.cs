using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.LogicalComparisonTests
{
    [TestClass]
    public class LessThanTests
    {
        [TestMethod]
        public void LessThan()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            data.AddRange<byte>(
                //set literal to 1 so left is 1
                (byte)InstructionsEnum.Literal,
                1,

                //set literal to 2 so right is 2
                (byte)InstructionsEnum.Literal,
                2,

                //compare 1 < 2
                (byte)InstructionsEnum.LessThan,

                //return
                (byte)InstructionsEnum.Return
            );
            
            var results = vm.Interpret(data);

            //confirm true
            Assert.AreEqual(1, results[0]);
        }

        [TestMethod]
        public void Equal()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            data.AddRange<byte>(
                //set literal to 1 so left is 1
                (byte)InstructionsEnum.Literal,
                1,

                //set literal to 1 so right is 1
                (byte)InstructionsEnum.Literal,
                1,

                //compare 1 < 1
                (byte)InstructionsEnum.LessThan,

                //return
                (byte)InstructionsEnum.Return
            );
            
            var results = vm.Interpret(data);

            //confirm false
            Assert.AreEqual(0, results[0]);
        }

        [TestMethod]
        public void GreaterThan()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            data.AddRange<byte>(
                //set literal to 2 so left is 2
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 1 so right is 1
                (byte)InstructionsEnum.Literal,
                1,

                //compare 2 < 1
                (byte)InstructionsEnum.LessThan,

                //return
                (byte)InstructionsEnum.Return
            );

            var results = vm.Interpret(data);

            //confirm false
            Assert.AreEqual(0, results[0]);
        }
    }
}
