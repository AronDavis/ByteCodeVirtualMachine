﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ByteCodeVirtualMachine.Tests
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
            };

            vm.Interpret(data);
        }

        [TestMethod]
        public void CreateAndSetWithLoop()
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

                    //1 field type
                    
                    //set literal to 1 for type id
                    (byte)InstructionsEnum.Literal,
                    1,

                    //set literal to 1 for number of fields
                    (byte)InstructionsEnum.Literal,
                    1,
                
                    //define type_1 with 1 field
                    (byte)InstructionsEnum.DefType,
                #endregion

                #region defArray
                    //set literal to 0 for type id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 0 for array id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 2 for length
                    (byte)InstructionsEnum.Literal,
                    2,

                    //define type_0[] array_0 = new type_0[]
                    (byte)InstructionsEnum.DefArray,
                #endregion

                #region setArray
                    //set literal to 1 for type_1
                    (byte)InstructionsEnum.Literal,
                    1,

                    //set literal to 0 for var id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //define var_0 of type_1
                    (byte)InstructionsEnum.DefVar,


                    //set literal to 0 for array id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //push the array length to the stack
                    (byte)InstructionsEnum.GetArrayLength,

                    //loop through each index in array
                    (byte)InstructionsEnum.For,

                    //get var 0 for x
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetVar,

                    //get var 0 for y
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetVar,

                    //set literal to 0 for var id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //get i for index
                    (byte)InstructionsEnum.GetVar,

                    //set literal to 0 for array id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //array_0[0] = [1,1]
                    (byte)InstructionsEnum.SetArrayValueAtIndex,


                    //set literal to 0 for var id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //get i so we can increment
                    (byte)InstructionsEnum.GetVar,

                    //set literal to 1 so we can add to index
                    (byte)InstructionsEnum.Literal,
                    1,

                    //add 1 to index
                    (byte)InstructionsEnum.Add,

                    
                    //set literal for var id to set
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set var_0 += 1
                    (byte)InstructionsEnum.SetVar,

                    (byte)InstructionsEnum.EndFor,
                #endregion
                
                #region getArrayValueAtIndex
                    //set literal to 0 for array id
                    (byte)InstructionsEnum.Literal,
                    0,

                    //set literal to 0 for index
                    (byte)InstructionsEnum.Literal,
                    0,
                    (byte)InstructionsEnum.GetArrayValueAtIndex,
                #endregion
            };

            vm.Interpret(data);
        }
    }
}