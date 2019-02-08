namespace ByteCodeVirtualMachine
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
        DefVar,
        GetVar,
        SetVar,
        GetStat,
        SetStat,
    }
}
