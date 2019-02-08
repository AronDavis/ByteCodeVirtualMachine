using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void CreateAndSet()
        {
            VirtualMachine vm = new VirtualMachine();

            byte[] data = new byte[]
            {
                #region defType
                    //set literal to 0 for type id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 2 for number of fields
                    (byte)InstructionsEnum.Literal,
                    2,
                
                    //define type_0 with 2 fields
                    (byte)InstructionsEnum.DefType,
                #endregion

                    //set an array of type_0 to be the return signature
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetType,
                    (byte)InstructionsEnum.Literal, // array = yes
                    1,
                    (byte)InstructionsEnum.ReturnSignature,
                    
                #region defArray
                    //set literal to 0 for type id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 0 for array id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 1 for length
                    (byte)InstructionsEnum.Literal,
                    1,

                    //define type_0[] array_0 = new type_0[]
                    (byte)InstructionsEnum.DefArray,
                #endregion

                #region setArray
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
                    (byte)InstructionsEnum.SetArrayValueAtIndex,
                #endregion

                    //return array_0
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.Return
            };

            var results = vm.Interpret(data);

            Assert.AreEqual(2, results.Length);

            Assert.AreEqual(3, results[0]);
            Assert.AreEqual(2, results[1]);
        }
    }
}
