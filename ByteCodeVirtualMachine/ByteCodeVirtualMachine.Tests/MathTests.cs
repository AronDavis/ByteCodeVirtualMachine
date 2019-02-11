using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Addition()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.Add()
                        .Left(3)
                        .Right(5);

                    b.Return();
                })
                .ToInstructions();

            var fluentResults = vm.Interpret(data);

            var results = vm.Interpret(data);

            //confirm 3 + 5
            Assert.AreEqual(8, results[0]);
        }

        [TestMethod]
        public void Subtraction()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.Subtract()
                        .Left(5)
                        .Right(3);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm 5-3
            Assert.AreEqual(2, results[0]);
        }

        [TestMethod]
        public void Multiplication()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.Multiply()
                        .Left(5)
                        .Right(3);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm 5 * 3
            Assert.AreEqual(15, results[0]);
        }

        [TestMethod]
        public void Division()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.Divide()
                        .Left(6)
                        .Right(2);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm 6/2
            Assert.AreEqual(3, results[0]);
        }
    }
}
