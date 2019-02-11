using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class CustomFunctionTests
    {
        private void _customFunction1(VirtualMachine vm)
        {
            byte val = vm.Pop();
            vm.Push((byte)(val + 2));
        }

        [TestMethod]
        public void CustomFunction()
        {
            VirtualMachine vm = new VirtualMachine();

            vm.CustomFunctions = new Action<VirtualMachine>[]
            {
                _customFunction1
            };

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            data.AddRange<byte>(
                //set literal to 1 so we can use it in customFunction_0
                (byte)InstructionsEnum.Literal,
                1,

                //set literal to 0 for customFunction_0
                (byte)InstructionsEnum.Literal,
                0,

                //use custom function
                (byte)InstructionsEnum.CustomFunction,

                //return
                (byte)InstructionsEnum.Return
            );

            var results = vm.Interpret(data);

            //confirm 1 + 2
            Assert.AreEqual(3, results[0]);
        }
    }
}
