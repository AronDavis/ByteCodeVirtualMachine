using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class ForTests
    {
        [TestMethod]
        public void For()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    //set literal to 0 so we have a start value to add
                    b.Literal(0);

                    b.For()
                        .NumberOfLoops(5) //loop 5 times
                        .Body(fb =>
                        {
                            //add 2 to the previous number
                            fb.Add()
                                .Right(2);
                        });

                    b.Return();
                })
                .ToInstructions();
            
            //confirm that five adds happened: 0 + 2 + 2 + 2 + 2 + 2
            var results = vm.Interpret(data);
            Assert.AreEqual(10, results[0]);
        }

        [TestMethod]
        public void NestedFor()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    //set literal to 0 so we have a start value to add
                    b.Literal(0);

                    b.For()
                        .NumberOfLoops(2) //loop 2 times
                        .Body(outterLoop =>
                        {
                            //add 5 to the previous number
                            outterLoop.Add()
                                .Right(5);

                            outterLoop.For()
                                .NumberOfLoops(10) //loop 10 times
                                .Body(innerLoop =>
                                {
                                    //add 3 to the previous number
                                    innerLoop.Add()
                                        .Right(3);
                                });
                        });

                    b.Return();
                })
                .ToInstructions();

            //confirm [(5) + (30)] + [(5) + (30)]
            var results = vm.Interpret(data);
            Assert.AreEqual(70, results[0]);
        }
    }
}
