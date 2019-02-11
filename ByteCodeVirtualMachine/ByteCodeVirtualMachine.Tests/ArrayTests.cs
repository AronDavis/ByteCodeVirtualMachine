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

            List<byte> data = new List<byte>();

            //define type_2
            data.AddRange<byte>(
                //set literal to 2 for type id
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 2 for number of fields
                (byte)InstructionsEnum.Literal,
                2,

                //define type_2 with 2 fields
                (byte)InstructionsEnum.DefType
            );

            //set an array of type_2 to be the return signature
            data.AddRange(TestHelper.GetReturnSignatureInstructions(2, true));

            //define type_2[] array_0 = new type_2[]
            data.AddRange<byte>(
                //set literal to 2 for type_2
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 0 for array id
                (byte)InstructionsEnum.Literal,
                0,

                //set literal to 1 for length
                (byte)InstructionsEnum.Literal,
                1,

                //define type_2[] array_0 = new type_2[]
                (byte)InstructionsEnum.DefArray
            );

            //set array_0[0] = [3, 2]
            data.AddRange<byte>(
                //set literal to 2 for value
                (byte)InstructionsEnum.Literal,
                2,

                //set literal to 3 for value
                (byte)InstructionsEnum.Literal,
                3,

                //set literal to 0 for array id
                (byte)InstructionsEnum.Literal,
                0,

                //set literal to 0 for index
                (byte)InstructionsEnum.Literal,
                0,

                //array_0[0] = [3, 2]
                (byte)InstructionsEnum.SetArrayValueAtIndex
            );

            //return array_0
            data.AddRange<byte>(
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetArray,
                    (byte)InstructionsEnum.Return
            );

            var results = vm.Interpret(data);

            Assert.AreEqual(2, results.Length);

            Assert.AreEqual(3, results[0]);
            Assert.AreEqual(2, results[1]);


            List<byte> fluentData = new InstructionsBuilder()
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

            var fluentResults = vm.Interpret(fluentData);

            TestHelper.AssertResultsEqual(results, fluentResults);
        }
    }
}
