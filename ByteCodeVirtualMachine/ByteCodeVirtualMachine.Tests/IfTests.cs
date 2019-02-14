using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class IfTests
    {
        [TestMethod]
        public void IfTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(true)
                        .Body(ib =>
                        {
                            //return 7
                            ib.Literal(7);
                            ib.Return();
                        });
                })
                .ToInstructions();
            
            //confirm 7
            var results = vm.Interpret(data);
            Assert.AreEqual(7, results[0]);
        }

        [TestMethod]
        public void IfFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(false)
                        .Body(ib =>
                        {
                            //return 7
                            ib.Literal(7);
                            ib.Return();
                        });

                    //return 3
                    b.Literal(3);
                    b.Return();
                })
                .ToInstructions();

            //confirm 3
            var results = vm.Interpret(data);
            Assert.AreEqual(3, results[0]);
        }

        [TestMethod]
        public void NestedIfBothTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(true)
                        .Body(outterIf =>
                        {
                            outterIf.If()
                                .Condition(true)
                                .Body(innerIf =>
                                {
                                    //return 7
                                    innerIf.Literal(7);
                                    innerIf.Return();
                                });
                        });
                })
                .ToInstructions();

            //confirm 7
            var results = vm.Interpret(data);
            Assert.AreEqual(7, results[0]);
        }

        [TestMethod]
        public void NestedIfBothFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(false)
                        .Body(outterIf =>
                        {
                            outterIf.If()
                                .Condition(false)
                                .Body(innerIf =>
                                {
                                    //return 7
                                    innerIf.Literal(7);
                                    innerIf.Return();
                                });
                        });

                    //return 3
                    b.Literal(3);
                    b.Return();
                })
                .ToInstructions();

            //confirm 3
            var results = vm.Interpret(data);
            Assert.AreEqual(3, results[0]);
        }

        [TestMethod]
        public void NestedIfTrueThenFalse()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(true)
                        .Body(outterIf =>
                        {
                            outterIf.If()
                                .Condition(false)
                                .Body(innerIf =>
                                {
                                    //return 7
                                    innerIf.Literal(7);
                                    innerIf.Return();
                                });

                            //return 3
                            outterIf.Literal(3);
                            outterIf.Return();
                        });

                    
                })
                .ToInstructions();

            //confirm 3
            var results = vm.Interpret(data);
            Assert.AreEqual(3, results[0]);
        }

        [TestMethod]
        public void NestedIfFalseThenTrue()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.If()
                        .Condition(false)
                        .Body(outterIf =>
                        {
                            outterIf.If()
                                .Condition(true)
                                .Body(innerIf =>
                                {
                                    //return 7
                                    innerIf.Literal(7);
                                    innerIf.Return();
                                });
                        });

                    //return 3
                    b.Literal(3);
                    b.Return();
                })
                .ToInstructions();

            //confirm 3
            var results = vm.Interpret(data);
            Assert.AreEqual(3, results[0]);
        }
    }
}