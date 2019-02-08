using System.Collections.Generic;

namespace BytecodeVirtualMachine.Tests
{
    //TODO: builder-style fluent API!
    internal static class TestHelper
    {
        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }

        internal static byte[] GetReturnSignatureInstructions(byte typeId, bool isArray = false)
        {
            if(typeId == 0)
                return new byte[]
                {
                    //set literal to define no return type
                    (byte)InstructionsEnum.Literal,
                    typeId,
                    (byte)InstructionsEnum.ReturnSignature,
                };

            return new byte[]
            {
                //set literal to define if return type is an array
                (byte)InstructionsEnum.Literal,
                (byte)(isArray ? 1 : 0),

                //set literal to define the return type
                (byte)InstructionsEnum.Literal,
                typeId,

                (byte)InstructionsEnum.ReturnSignature,
            };
        }

    }
}
