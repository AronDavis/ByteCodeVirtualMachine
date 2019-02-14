using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests.LogicalComparisonTests
{
    [TestClass]
    public class LessThanOrEqualToTests
    {
        [TestMethod]
        public void LessThan()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.LessThanOrEqualTo()
                        .Left(1)
                        .Right(2);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm true
            Assert.AreEqual(1, results[0]);
        }

        [TestMethod]
        public void Equal()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.LessThanOrEqualTo()
                        .Left(1)
                        .Right(1);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm true
            Assert.AreEqual(1, results[0]);
        }

        [TestMethod]
        public void GreaterThan()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.LessThanOrEqualTo()
                        .Left(2)
                        .Right(1);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm false
            Assert.AreEqual(0, results[0]);
        }
    }
}
