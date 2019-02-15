using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine
{
    public class VirtualMachine
    {
        public const int MAX_STACK_SIZE = 128;
        private int _stackSize = 0;
        private byte[] _stack = new byte[MAX_STACK_SIZE];

        //TODO: move all of this stuff to be inside of BytecodeFunction so we can have things like scope?

        private Stack<bool> ifs = new Stack<bool>();
        private Stack<int> indexesWhereForStarted = new Stack<int>();
        private Stack<byte> forCounts = new Stack<byte>();
        private BytecodeFunction[] _functions = new BytecodeFunction[MAX_STACK_SIZE];

        private BytecodeArray[] _arrays = new BytecodeArray[byte.MaxValue + 1];

        //TODO: could just replace this with the corresponding byte being the number of field...so 1 would have 1 field, 2 would have 2 fields, etc.
        private byte[] _types = new byte[byte.MaxValue + 1];

        //TODO: vars and arrays should probably be the same thing
        private BytecodeClass[] vars = new BytecodeClass[byte.MaxValue + 1];

        public Action<VirtualMachine>[] CustomFunctions;

        public VirtualMachine()
        {
            //type_0 is a placeholder for void/null
            _types[0] = 0;

            //type_1 is a native type (single byte)
            _types[1] = 1;
        }

        private byte[] _interpretFunction(BytecodeFunction function)
        {
            Console.WriteLine(this);
            int size = function.Instructions.Count;
            for (int i = 0; i < size; i++)
            {
                InstructionsEnum instruction = (InstructionsEnum)function.Instructions[i];

                //if we're in an if and the status is false
                if (ifs.Count > 0 && ifs.Peek() == false)
                {
                    //skip all instructions until we hit end if (still shift i for literals though)
                    switch (instruction)
                    {
                        case InstructionsEnum.If:
                            ifs.Push(false); //push a false so we can handle nested ifs
                            continue;
                        case InstructionsEnum.Literal:
                            i++;
                            continue;
                        case InstructionsEnum.EndIf:
                            break;
                        default:
                            continue;
                    }
                }

                switch (instruction)
                {
                    case InstructionsEnum.Literal:
                        _literal(function.Instructions, ref i);
                        break;
                    case InstructionsEnum.ReturnSignature:
                        _returnSignature(function);
                        break;
                    case InstructionsEnum.Add: //should be preceded by two values to add
                        _add();
                        break;
                    case InstructionsEnum.Subtract: //should be preceded by two values to subtract
                        _subtract();
                        break;
                    case InstructionsEnum.Multiply: //should be preceded by two values to multiply
                        _multiply();
                        break;
                    case InstructionsEnum.Divide: //should be preceded by two values to divide
                        _divide();
                        break;
                    case InstructionsEnum.LessThan:
                        _lessThan();
                        break;
                    case InstructionsEnum.GreaterThan:
                        _greaterThan();
                        break;
                    case InstructionsEnum.EqualTo:
                        _equalTo();
                        break;
                    case InstructionsEnum.GreaterThanOrEqualTo:
                        _greaterThanOrEqualTo();
                        break;
                    case InstructionsEnum.LessThanOrEqualTo:
                        _lessThanOrEqualTo();
                        break;
                    case InstructionsEnum.If:
                        _if();
                        break;
                    case InstructionsEnum.EndIf:
                        _endIf();
                        break;
                    case InstructionsEnum.For:
                        _for(i);
                        break;
                    case InstructionsEnum.EndFor:
                        _endFor(ref i);
                        break;
                    case InstructionsEnum.DefArray:
                        _defArray();
                        break;
                    case InstructionsEnum.GetArray:
                        _getArray();
                        break;
                    case InstructionsEnum.GetArrayLength:
                        _getArrayLength();
                        break;
                    case InstructionsEnum.GetArrayValueAtIndex:
                        _getArrayValueAtIndex();
                        break;
                    case InstructionsEnum.SetArrayValueAtIndex:
                        _setArrayValueAtIndex();
                        break;
                    case InstructionsEnum.DefType:
                        _defType();
                        break;
                    case InstructionsEnum.DefVar:
                        _defVar();
                        break;
                    case InstructionsEnum.GetVar:
                        _getVar();
                        break;
                    case InstructionsEnum.SetVar:
                        _setVar();
                        break;
                    case InstructionsEnum.DefFunction:
                        _defFunction(function.Instructions, ref i);
                        break;
                    case InstructionsEnum.Function:
                        _function();
                        break;
                    case InstructionsEnum.CustomFunction:
                        _customFunction();
                        break;
                    case InstructionsEnum.Return:
                        return _return(function);
                }

                Console.WriteLine(this);
            }

            if (function.ReturnType > 0)
                throw new Exception("Missing return statement.");

            return null;
        }

        public byte[] Interpret(IList<byte> bytes) //TODO: should take a stack?
        {
            BytecodeFunction function = new BytecodeFunction(bytes);
            _interpretFunction(function);

            return _interpretFunction(_functions[0]);
        }

        private void _literal(List<byte> bytes, ref int i)
        {
            // Read the next byte from the bytecode.
            byte value = bytes[++i];

            //push to stack
            Push(value);

            Console.WriteLine("Setting literal to " + value);
        }

        private void _returnSignature(BytecodeFunction function)
        {
            function.ReturnType = Pop();

            function.ShouldReturnArray = (Pop() == 1);
            
            if (function.ShouldReturnArray)
                Console.WriteLine($"Setting signature to return type_{function.ReturnType}[]");
            else
                Console.WriteLine($"Setting signature to return type_{function.ReturnType}");
        }

        private void _add()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left + right)); //assume we won't overflow

            Console.WriteLine("Adding " + right + " + " + left);
        }

        private void _subtract()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left - right)); //assume we won't overflow
        }

        private void _multiply()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left * right)); //assume we won't overflow
        }

        private void _divide()
        {
            byte right = Pop();
            byte left = Pop();
            
            Push((byte)(left / right)); //assume we won't overflow
        }

        private void _lessThan()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left < right ? 1 : 0));
        }

        private void _greaterThan()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left > right ? 1 : 0));
        }

        private void _equalTo()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left == right ? 1 : 0));
        }

        private void _greaterThanOrEqualTo()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left >= right ? 1 : 0));
        }
        private void _lessThanOrEqualTo()
        {
            byte right = Pop();
            byte left = Pop();

            Push((byte)(left <= right ? 1 : 0));
        }

        private void _if()
        {
            byte value = Pop();
            ifs.Push(value != 0);
        }

        private void _endIf()
        {
            ifs.Pop();
        }

        private void _for(int i)
        {
            byte value = Pop();

            indexesWhereForStarted.Push(i);
            forCounts.Push(value);

            Console.WriteLine("Loop " + value + " times.");
        }

        private void _endFor(ref int i)
        {
            byte currentCount = forCounts.Pop();
            //if this was the last loop
            if (currentCount == 1)
            {
                //pop the index because we're done with this for loop
                indexesWhereForStarted.Pop();

                Console.WriteLine("Exit loop.");
            }
            else
            {
                //decrement the count
                forCounts.Push(--currentCount);

                //start loop over
                i = indexesWhereForStarted.Peek();

                Console.WriteLine("Back to top of loop.");
            }
        }

        private void _defArray()
        {
            byte length = Pop();
            byte id = Pop();
            byte typeId = Pop();

            if (typeId == 0)
                throw new Exception("Cannot have null (type_0) array type.");

            byte numFields = _types[typeId];

            BytecodeArray array = new BytecodeArray()
            {
                Type = typeId,
                Items = new BytecodeClass[length]
            };

            for (int i = 0; i < length; i++)
            {
                array.Items[i] = new BytecodeClass()
                {
                    Fields = new byte[numFields]
                };
            }

            _arrays[id] = array;

            Console.WriteLine($"type_{typeId}[] array_{id} = new type_{typeId}[{length}]");
        }

        private void _getArray()
        {
            byte arrayId = Pop();

            BytecodeArray array = _arrays[arrayId];
            byte numFields = _types[array.Type];

            for (int i = 0; i < array.Items.Length; i++)
            {
                BytecodeClass item = array.Items[i];
                for (int j = 0; j < numFields; j++)
                    Push(item.Fields[j]);
            }

            //push length so we can read it back
            Push((byte)array.Items.Length);

            Console.WriteLine($"Retrieved array_{arrayId}.");
        }

        private void _getArrayLength()
        {
            byte id = Pop();

            byte length = (byte)_arrays[id].Items.Length;

            Console.WriteLine("array_" + id + " has length of " + length);
            Push(length);
        }

        private void _getArrayValueAtIndex()
        {
            byte itemIndex = Pop();
            byte arrayId = Pop();

            BytecodeArray array = _arrays[arrayId];
            BytecodeClass item = array.Items[itemIndex];

            Console.Write($"array_{arrayId}[{itemIndex}] has a value of [");
            for (int i = 0; i < item.Fields.Length; i++)
            {
                byte value = item.Fields[i];
                Push(value);

                if (i == item.Fields.Length - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _setArrayValueAtIndex()
        {
            byte itemIndex = Pop();
            byte arrayId = Pop();

            BytecodeArray array = _arrays[arrayId];
            BytecodeClass item = array.Items[itemIndex];

            Console.Write($"array_{arrayId}[{itemIndex}] = [");
            for (int field = 0; field < item.Fields.Length; field++)
            {
                byte value = Pop();
                item.Fields[field] = value;

                if (field == item.Fields.Length - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _defType()
        {
            byte numBytes = Pop();
            byte id = Pop();

            if (id <= 1)
                throw new Exception("type_0 and type_1 are native types and cannot be overwritten.");

            //push to arrays table
            _types[id] = numBytes;

            Console.WriteLine($"type_{id} = new type[{numBytes}]");
        }

        private void _defVar()
        {
            byte id = Pop();
            byte typeId = Pop();

            if (typeId == 0)
                throw new Exception("Cannot have a null (type_0) variable type.");

            byte numFields = _types[typeId];

            vars[id] = new BytecodeClass()
            {
                Type = typeId,
                Fields = new byte[numFields]
            };

            Console.WriteLine($"type_{typeId} var_{id} = new type_{typeId}()");
        }

        private void _getVar()
        {
            byte id = Pop();

            BytecodeClass variable = vars[id];
            byte numFields = _types[variable.Type];

            Console.Write($"var_{id} has a value of [");
            for (int i = 0; i < numFields; i++)
            {
                byte value = variable.Fields[i];
                Push(value);

                if (i == numFields - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _setVar()
        {
            byte id = Pop();

            BytecodeClass variable = vars[id];
            byte numFields = _types[variable.Type];

            Console.Write($"var_{id} = [");
            for (int i = 0; i < numFields; i++)
            {
                byte value = Pop();
                variable.Fields[i] = value;

                if (i == numFields - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _defFunction(List<byte> bytes, ref int i)
        {
            byte id = Pop();

            BytecodeFunction function = new BytecodeFunction();

            byte functionCount = 1;

            bool shouldEscapeWhileLoop = false;
            while (i < bytes.Count)
            {
                // Read the next byte from the bytecode.
                byte value = bytes[++i];
                InstructionsEnum instruction = (InstructionsEnum)value;

                //skip all instructions until we hit end function (still shift i for literals though)
                switch (instruction)
                {
                    case InstructionsEnum.DefFunction:
                        functionCount++;
                        //TODO: consider order and things
                        function.Instructions.Add(value);
                        break;
                    case InstructionsEnum.EndDefFunction:
                        functionCount--;
                        if (functionCount == 0)
                            shouldEscapeWhileLoop = true;
                        else
                            function.Instructions.Add(value);
                        break;
                    case InstructionsEnum.Literal:
                        //TODO: consider order and things
                        function.Instructions.Add(value);
                        function.Instructions.Add(bytes[++i]);
                        break;
                    default:
                        //TODO: consider order and things
                        function.Instructions.Add(value);
                        break;
                }

                if (shouldEscapeWhileLoop)
                    break;                
            }
            
            Console.WriteLine($"Defining function_{id}");
            _functions[id] = function;
        }

        private void _function()
        {
            byte id = Pop();

            BytecodeFunction function = _functions[id];

            if (function == null)
                throw new Exception($"function_{id} not defined.");

            Console.WriteLine($"Running function_{id}");

            byte[] results = _interpretFunction(function);

            if (results == null || results.Length == 0)
                return;

            //TODO: check that this is all happening in order
            for (int i = 0; i < results.Length; i++)
                Push(results[0]);
        }

        private void _customFunction()
        {
            byte id = Pop();

            if (CustomFunctions == null || CustomFunctions.Length - 1 < id)
                throw new Exception($"Custom Function {id} is not defined.");

            Action<VirtualMachine> customFunction = CustomFunctions[id];

            if(customFunction == null)
                throw new Exception($"Custom Function {id} is not defined.");

            Console.WriteLine($"Running Custom Function {id}");
            customFunction(this);
        }

        private byte[] _return(BytecodeFunction function)
        {
            if (function.ReturnType == 0)
            {
                Console.Write($"Returning null");
                return null;
            }

            byte[] returnBytes;

            if (function.ShouldReturnArray)
            {
                byte arrayLength = Pop();
                byte numFields = _types[function.ReturnType];

                Console.WriteLine($"Returning a type_{function.ReturnType}[]");

                returnBytes = new byte[arrayLength * numFields];

                for (int i = 0; i < arrayLength; i++)
                {
                    for (int j = 0; j < numFields; j++)
                        returnBytes[(i * numFields) + (numFields-1-j)] = Pop(); //(numFields-1-j) is to ensure that we reverse the order they would otherwise appear in in the stack
                }
                    
                return returnBytes;
            }

            Console.WriteLine($"Returning a type_{function.ReturnType}");

            returnBytes = new byte[function.ReturnType];

            for (byte i = 0; i < function.ReturnType; i++)
                returnBytes[i] = Pop();

            return returnBytes;
        }

        public void Push(byte value)
        {
            //check for overflow
            if (_stackSize >= MAX_STACK_SIZE)
                throw new OverflowException("Cannot push to a full stack!");

            _stack[_stackSize++] = value;
        }

        public byte Pop()
        {
            //check if empty
            if (_stackSize <= 0)
                throw new IndexOutOfRangeException("Cannot pop from an empty stack!");

            return _stack[--_stackSize];
        }

        public override string ToString()
        {
            string output = "Stack: [";
            for (int i = 0; i < _stackSize; i++)
            {
                byte value = _stack[i];

                if (i == _stackSize - 1)
                    output += value;
                else
                    output += value + ", ";
            }
            output += "]";
            return output;
        }
    }
}
