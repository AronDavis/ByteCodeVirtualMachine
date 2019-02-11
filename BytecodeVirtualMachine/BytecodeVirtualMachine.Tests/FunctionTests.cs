using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            //define var_0
            data.AddRange<byte>(
                //set literal to 1 for type_1
                (byte)InstructionsEnum.Literal,
                1,

                //set literal to 0 for var id
                (byte)InstructionsEnum.Literal,
                0,

                //define var_0 of type_1
                (byte)InstructionsEnum.DefVar
            );

            //function definition
            data.AddRange<byte>(
                //set literal to 0 for function_0
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.DefFunction,

                //get var_0
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.GetVar,

                //set literal to 1 so we can add 1
                (byte)InstructionsEnum.Literal,
                1,

                //add 1 to var_0
                (byte)InstructionsEnum.Add,

                //literal for id of var we'll be setting (var_0)
                (byte)InstructionsEnum.Literal,
                0,

                //var_0++
                (byte)InstructionsEnum.SetVar,

                //end function definition
                (byte)InstructionsEnum.EndDefFunction
            );

            for(int i = 0; i < 3; i++)
            {
                //run function_0 3 times
                data.AddRange<byte>(
                    //set literal to 0 for function_0
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.Function
                );
            }

            //return var_0
            data.AddRange<byte>(
                //get var_0
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.GetVar,

                //return
                (byte)InstructionsEnum.Return
            );

            var results = vm.Interpret(data);

            //confirm var_0 is 3
            Assert.AreEqual(3, results[0]);
        }

        [TestMethod]
        public void FunctionWithReturn()
        {
            VirtualMachine vm = new VirtualMachine();

            List<byte> data = new List<byte>();

            //set return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            //start function definition
            data.AddRange<byte>(
                //set literal to 0 for function_0
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.DefFunction
            );

            //set function return type to type_1
            data.AddRange(TestHelper.GetReturnSignatureInstructions(1));

            //finish function definition
            data.AddRange<byte>(
                //set literal to 1 so we can add 1
                (byte)InstructionsEnum.Literal,
                1,

                //add 1
                (byte)InstructionsEnum.Add,
                
                //return
                (byte)InstructionsEnum.Return,

                //end function definition
                (byte)InstructionsEnum.EndDefFunction
            );

            data.AddRange<byte>(
                //set literal to 0 as a starting point for addition
                (byte)InstructionsEnum.Literal,
                0
            );

            for (int i = 0; i < 3; i++)
            {
                //run function_0 3 times
                data.AddRange<byte>(
                    //set literal to 0 for function_0
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.Function
                );
            }

            //return
            data.Add((byte)InstructionsEnum.Return);

            var results = vm.Interpret(data);

            //confirm that result is 3
            Assert.AreEqual(3, results[0]);
        }
    }
}
