namespace BytecodeVirtualMachine
{
    public enum InstructionsEnum : byte
    {
        Literal,
        Add,
        Subtract,
        Multiply,
        Divide,
        If,
        EndIf,
        For,
        EndFor,
        DefArray,
        GetArrayLength,
        GetArrayValueAtIndex,
        SetArrayValueAtIndex,
        DefType,
        GetType,
        DefVar,
        GetVar,
        SetVar,
        ReturnSignature,
        Return
    }
}
