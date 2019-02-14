using BytecodeVirtualMachine.FluentInterface;
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

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    //set literal to 1 so we can use it in customFunction_0
                    b.Literal(1);

                    b.CustomFunction()
                        .Id(0);
                    
                    b.Return();
                })
                .ToInstructions();
            
            var results = vm.Interpret(data);

            //confirm 1 + 2
            Assert.AreEqual(3, results[0]);
        }
    }
}
