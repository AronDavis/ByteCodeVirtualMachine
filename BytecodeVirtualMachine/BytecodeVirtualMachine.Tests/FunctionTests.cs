using BytecodeVirtualMachine.FluentInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void FunctionWithNoReturn()
        {
            VirtualMachine vm = new VirtualMachine();

            byte varId = 0;
            byte functionId = 1;

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    //define var_0 of type_1
                    b.DefVar()
                        .Id(varId)
                        .Type(1);

                    b.DefFunction()
                        .Id(functionId)
                        .Body(fb =>
                        {
                            //get var
                            fb.GetVar()
                                .Id(varId);

                            //add 1
                            fb.Add()
                                .Right(1);

                            //save var_0
                            fb.SetVar()
                                .Id(varId);
                        });

                    //run function_1 three times
                    b.For()
                        .NumberOfLoops(3)
                        .Body(fb =>
                        {
                            fb.Function()
                                .Id(functionId);
                        });

                    //return var_0
                    b.GetVar()
                        .Id(varId);

                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm var_0 is 3
            Assert.AreEqual(3, results[0]);
        }

        [TestMethod]
        public void FunctionWithReturn()
        {
            VirtualMachine vm = new VirtualMachine();

            byte functionId = 1;

            List<byte> data = new InstructionsBuilder()
                .Main()
                .ReturnSignature(1)
                .Body(b =>
                {
                    b.DefFunction()
                        .Id(functionId)
                        .ReturnSignature(1)
                        .Body(fb =>
                        {
                            fb.Add()
                                .Right(1);

                            fb.Return();
                        });

                    //set literal to 0 as a starting point for addition
                    b.Literal(0);

                    //run function_1 three times
                    b.For()
                        .NumberOfLoops(3)
                        .Body(fb =>
                        {
                            fb.Function()
                                .Id(functionId);
                        });
                    
                    b.Return();
                })
                .ToInstructions();

            var results = vm.Interpret(data);

            //confirm that result is 3
            Assert.AreEqual(3, results[0]);
        }
    }
}
