using BytecodeVirtualMachine.FluentInterface.Instructions;

namespace BytecodeVirtualMachine.FluentInterface
{
    public class InstructionsBuilder
    {
        public MainFunctionBuilder Main()
        {
            return new MainFunctionBuilder();
        }
    }
}
