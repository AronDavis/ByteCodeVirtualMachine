﻿namespace BytecodeVirtualMachine
{
    public enum InstructionsEnum : byte
    {
        Literal,
        Add,
        Subtract,
        Multiply,
        Divide,
        LessThan,
        GreaterThan,
        EqualTo,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo,
        If,
        EndIf,
        For,
        EndFor,
        DefArray,
        GetArray,
        GetArrayLength,
        GetArrayValueAtIndex,
        SetArrayValueAtIndex,
        DefType,
        DefVar,
        GetVar,
        SetVar,
        DefFunction,
        EndDefFunction,
        Function,
        CustomFunction,
        ReturnSignature,
        Return
    }
}
