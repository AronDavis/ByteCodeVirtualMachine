using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void CreateAndSet()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(2, true)
                .Body(b =>
                {
                    byte typeId = 2;
                    byte arrayId = 0;

                    b.DefType()
                        .Id(typeId)
                        .NumberOfFields(2);

                    b.DefArray()
                        .Type(typeId)
                        .Id(arrayId)
                        .Length(1);

                    b.SetArrayValueAtIndex()
                        .Value(2)
                        .Value(3)
                        .Id(arrayId)
                        .Index(0);

                    b.GetArray()
                        .Id(arrayId);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            Assert.AreEqual(2, results.Length);

            Assert.AreEqual(3, results[0]);
            Assert.AreEqual(2, results[1]);
        }
    }
}
