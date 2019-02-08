using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BytecodeVirtualMachine.Tests
{
    [TestClass]
    public class VariableTests
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

                    //set literal to 1 for number of fields
                    (byte)InstructionsEnum.Literal,
                    1,
                
                    //define type_0 with 2 fields
                    (byte)InstructionsEnum.DefType,
                #endregion

                //set an type_0 as the return type
                (byte)InstructionsEnum.Literal,
                0,
                (byte)InstructionsEnum.GetType,
                (byte)InstructionsEnum.Literal, // array = no
                0,
                (byte)InstructionsEnum.ReturnSignature,
                
                #region defVariable
                    //set literal to 1 for type_1
                    (byte)InstructionsEnum.Literal,
                    1,

                    //set literal to 0 for var id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //define var_0 of type_1
                    (byte)InstructionsEnum.DefVar,
                #endregion

                #region setVariable
                    //set literal to 1 so we set var field to 1
                    (byte)InstructionsEnum.Literal,
                    1,

                    //set literal for id of var we'll be using (var_0)
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set var_0 to = 1
                    (byte)InstructionsEnum.SetVar,
                #endregion

                #region return
                    //set literal to 0 for var_0
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetVar,
                    (byte)InstructionsEnum.Return
                #endregion
            };

            var results = vm.Interpret(data);

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(1, results[0]);
        }
    }
}
