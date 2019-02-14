using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void CreateAndSet()
        {
            VirtualMachine vm = new VirtualMachine();

            byte varId = 0;

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    //define var_0 of type_1
                    b.DefVar()
                        .Id(varId)
                        .Type(1);

                    //set var_0 to = 1
                    b.SetVar()
                        .Id(varId)
                        .Value(1);

                    //get var_0
                    b.GetVar()
                        .Id(varId);
                    
                    b.Return();
                })
                .ToInstructions();
            
            var results = vm.Interpret(data);

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(1, results[0]);
        }
    }
}
