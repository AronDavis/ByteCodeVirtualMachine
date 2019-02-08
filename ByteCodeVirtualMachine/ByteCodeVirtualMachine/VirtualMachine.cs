using System;
using System.Collections.Generic;

namespace BytecodeVirtualMachine
{
    //TODO: allow custom functions (CustomFunctionEnum, #) then insert custom functions via an exposed Array
    public class VirtualMachine
    {
        public const int MAX_STACK_SIZE = 128;
        private int _stackSize = 0;
        private byte[] _stack = new byte[MAX_STACK_SIZE];
        private Stack<bool> ifs = new Stack<bool>();
        private Stack<int> indexesWhereForStarted = new Stack<int>();
        private Stack<byte> forCounts = new Stack<byte>();

        private byte _returnType = 0;
        private bool _shouldReturnArray = false;

        private BytecodeArray[] _arrays = new BytecodeArray[byte.MaxValue + 1];

        private byte[] _types = new byte[byte.MaxValue + 1]; //TODO: could just replace this with the corresponding byte being the number of field...so 1 would have 1 field, 2 would have 2 fields, etc.

        private BytecodeClass[] vars = new BytecodeClass[byte.MaxValue + 1];

        public VirtualMachine()
        {
            //type_0 is a placeholder for void/null
            _types[0] = 0;

            //type_1 is a native type (single byte)
            _types[1] = 1;
        }

        public byte[] Interpret(IList<byte> bytes)
        {
            Console.WriteLine(this);
            int size = bytes.Count;
            for (int i = 0; i < size; i++)
            {
                InstructionsEnum instruction = (InstructionsEnum)bytes[i];

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
                        _literal(bytes, ref i);
                        break;
                    case InstructionsEnum.ReturnSignature:
                        _returnSignature();
                        break;
                    case InstructionsEnum.Add: //should be preceded by two values to add
                        _add();
                        break;
                    case InstructionsEnum.Subtract: //should be preceded by two values to add
                        _subtract();
                        break;
                    case InstructionsEnum.Multiply:
                        _multiply();
                        break;
                    case InstructionsEnum.Divide:
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
                    case InstructionsEnum.Return:
                        return _return();
                }

                Console.WriteLine(this);
            }

            if (_returnType > 0)
                throw new Exception("Missing return statement.");
            
            return null;
        }

        private void _literal(IList<byte> bytes, ref int i)
        {
            // Read the next byte from the bytecode.
            byte value = bytes[++i];

            //push to stack
            _push(value);

            Console.WriteLine("Setting literal to " + value);
        }

        private void _returnSignature()
        {
            _returnType = _pop();

            if(_returnType > 0)
                _shouldReturnArray = _pop() == 1;
            
            if (_shouldReturnArray)
                Console.Write($"Setting signature to return type_{_returnType}[]");
            else
                Console.Write($"Setting signature to return type_{_returnType}");
        }

        private void _add()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left + right)); //assume we won't overflow

            Console.WriteLine("Adding " + right + " + " + left);
        }

        private void _subtract()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left - right)); //assume we won't overflow
        }

        private void _multiply()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left * right)); //assume we won't overflow
        }

        private void _divide()
        {
            byte right = _pop();
            byte left = _pop();
            
            _push((byte)(left / right)); //assume we won't overflow
        }

        private void _lessThan()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left < right ? 1 : 0));
        }

        private void _greaterThan()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left > right ? 1 : 0));
        }

        private void _equalTo()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left == right ? 1 : 0));
        }

        private void _greaterThanOrEqualTo()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left >= right ? 1 : 0));
        }
        private void _lessThanOrEqualTo()
        {
            byte right = _pop();
            byte left = _pop();

            _push((byte)(left <= right ? 1 : 0));
        }

        private void _if()
        {
            byte value = _pop();
            ifs.Push(value != 0);
        }

        private void _endIf()
        {
            ifs.Pop();
        }

        private void _for(int i)
        {
            byte value = _pop();

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
            byte length = _pop();
            byte id = _pop();
            byte typeId = _pop();

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
            byte arrayId = _pop();

            BytecodeArray array = _arrays[arrayId];
            byte numFields = _types[array.Type];

            for (int i = 0; i < array.Items.Length; i++)
            {
                BytecodeClass item = array.Items[i];
                for (int j = 0; j < numFields; j++)
                    _push(item.Fields[j]);
            }

            //push length so we can read it back
            _push((byte)array.Items.Length);

            Console.WriteLine($"Retrieved array_{arrayId}.");
        }

        private void _getArrayLength()
        {
            byte id = _pop();

            byte length = (byte)_arrays[id].Items.Length;

            Console.WriteLine("array_" + id + " has length of " + length);
            _push(length);
        }

        private void _getArrayValueAtIndex()
        {
            byte itemIndex = _pop();
            byte arrayId = _pop();

            BytecodeArray array = _arrays[arrayId];
            BytecodeClass item = array.Items[itemIndex];

            Console.Write($"array_{arrayId}[{itemIndex}] has a value of [");
            for (int i = 0; i < item.Fields.Length; i++)
            {
                byte value = item.Fields[i];
                _push(value);

                if (i == item.Fields.Length - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _setArrayValueAtIndex()
        {
            byte itemIndex = _pop();
            byte arrayId = _pop();

            BytecodeArray array = _arrays[arrayId];
            BytecodeClass item = array.Items[itemIndex];

            Console.Write($"array_{arrayId}[{itemIndex}] = [");
            for (int field = 0; field < item.Fields.Length; field++)
            {
                byte value = _pop();
                item.Fields[field] = value;

                if (field == item.Fields.Length - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _defType()
        {
            byte numBytes = _pop();
            byte id = _pop();

            if (id <= 1)
                throw new Exception("type_0 and type_1 are native types and cannot be overwritten.");

            //push to arrays table
            _types[id] = numBytes;

            Console.WriteLine($"type_{id} = new type[{numBytes}]");
        }

        private void _defVar()
        {
            byte id = _pop();
            byte typeId = _pop();

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
            byte id = _pop();

            BytecodeClass variable = vars[id];
            byte numFields = _types[variable.Type];

            Console.Write($"var_{id} has a value of [");
            for (int i = 0; i < numFields; i++)
            {
                byte value = variable.Fields[i];
                _push(value);

                if (i == numFields - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private void _setVar()
        {
            byte id = _pop();

            BytecodeClass variable = vars[id];
            byte numFields = _types[variable.Type];

            Console.Write($"var_{id} = [");
            for (int i = 0; i < numFields; i++)
            {
                byte value = _pop();
                variable.Fields[i] = value;

                if (i == numFields - 1)
                    Console.WriteLine($"{value}]");
                else
                    Console.Write($"{value}, ");
            }
        }

        private byte[] _return()
        {
            if (_returnType == 0)
            {
                Console.Write($"Returning null");
                return null;
            }

            byte[] returnBytes;

            if (_shouldReturnArray)
            {
                byte arrayLength = _pop();
                byte numFields = _types[_returnType];

                Console.Write($"Returning a type_{_returnType}[]");

                returnBytes = new byte[arrayLength * numFields];

                for (int i = 0; i < arrayLength; i++)
                {
                    for (int j = 0; j < numFields; j++)
                        returnBytes[(i * numFields) + (numFields-1-j)] = _pop(); //(numFields-1-j) is to ensure that we reverse the order they would otherwise appear in in the stack
                }
                    
                return returnBytes;
            }

            Console.Write($"Returning a type_{_returnType}");

            returnBytes = new byte[_returnType];

            for (byte i = 0; i < _returnType; i++)
                returnBytes[i] = _pop();

            return returnBytes;
        }

        private void _push(byte value)
        {
            //check for overflow
            if (_stackSize >= MAX_STACK_SIZE)
                throw new OverflowException("Cannot push to a full stack!");

            _stack[_stackSize++] = value;
        }

        private byte _pop()
        {
            //check if empty
            if (_stackSize <= 0)
                throw new IndexOutOfRangeException("Cannot pop from an empty stack!");

            return _stack[--_stackSize];
        }

        private byte _peek()
        {
            //check if empty
            if (_stackSize <= 0)
                throw new IndexOutOfRangeException("Cannot peek an empty stack!");

            return _stack[_stackSize - 1];
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
