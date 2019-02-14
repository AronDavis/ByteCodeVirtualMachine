using BytecodeVirtualMachine.FluentInterface.Instructions;

namespace BytecodeVirtualMachine.FluentInterface
{
    public class InstructionsBuilder
    {
        private DefFunctionInstruction _main;

        public InstructionsBuilder()
        {
            _main = new DefFunctionInstruction().Id(0);
        }

        public DefFunctionInstruction Main()
        {
            return _main;
        }
    }
}
