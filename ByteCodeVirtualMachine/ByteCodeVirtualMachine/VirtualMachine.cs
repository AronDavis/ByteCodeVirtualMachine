using System;
using System.Collections.Generic;

namespace ByteCodeVirtualMachine
{
    public class VirtualMachine
    {
        public const int MAX_STACK_SIZE = 128;
        private int _stackSize = 0;
        private byte[] _stack = new byte[MAX_STACK_SIZE];
        private Stack<bool> ifs = new Stack<bool>();
        private Stack<int> indexesWhereForStarted = new Stack<int>();
        private Stack<byte> forCounts = new Stack<byte>();

        private byte[][][] arrays = new byte[byte.MaxValue + 1][][];
        private byte[] arrayTypes = new byte[byte.MaxValue + 1];
        private byte[] types = new byte[byte.MaxValue + 1]; //TODO: could just replace this with the corresponding byte being the number of field...so 1 would have 1 field, 2 would have 2 fields, etc.

        private byte[][] vars = new byte[byte.MaxValue + 1][]; //could be a dictionary...
        private byte[] varTypes = new byte[byte.MaxValue + 1]; //could be a dictionary...

        //TODO: this should return an int or an array of ints (instead of using Peek)
        public void Interpret(byte[] bytes)
        {
            Console.WriteLine(this);
            int size = bytes.Length;
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
                }

                Console.WriteLine(this);
            }
        }

        private void _literal(byte[] bytes, ref int i)
        {
            // Read the next byte from the bytecode.
            byte value = bytes[++i];

            //push to stack
            _push(value);

            Console.WriteLine("Setting literal to " + value);
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

            //set the type of the array so user doesn't have to pass it in again on set
            arrayTypes[id] = typeId;

            byte numFields = types[typeId];

            //push to arrays table
            arrays[id] = new byte[length][];

            for (int item = 0; item < length; item++)
            {
                arrays[id][item] = new byte[numFields];
            }

            Console.WriteLine("type_" + typeId + "[] array_" + id + " = new type_" + typeId + "[" + length + "]");
        }

        private void _getArrayLength()
        {
            byte id = _pop();

            byte length = (byte)arrays[id].Length;

            Console.WriteLine("array_" + id + " has length of " + length);
            _push(length);
        }

        private void _getArrayValueAtIndex()
        {
            byte index = _pop();
            byte id = _pop();

            byte typeId = arrayTypes[id];

            byte numFields = types[typeId];

            Console.Write("array_" + id + "[" + index + "] has a value of [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = arrays[id][index][field];
                _push(value);

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void _setArrayValueAtIndex()
        {
            byte id = _pop();
            byte index = _pop();

            byte typeId = arrayTypes[id];

            byte numFields = types[typeId];

            Console.Write("array_" + id + "[" + index + "]" + " = [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = _pop();
                arrays[id][index][field] = value;

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void _defType()
        {
            byte numBytes = _pop();
            byte id = _pop();

            //push to arrays table
            types[id] = numBytes;

            Console.WriteLine("type_" + id + " = new type[" + numBytes + "]");
        }

        private void _defVar()
        {
            byte id = _pop();
            byte typeId = _pop();

            varTypes[id] = typeId;

            byte numFields = types[typeId];

            vars[id] = new byte[numFields];

            Console.WriteLine("type_" + typeId + " var_" + id + " = new type_" + typeId + "()");
        }

        private void _getVar()
        {
            byte id = _pop();

            byte typeId = varTypes[id];

            byte numFields = types[typeId];

            Console.Write("var_" + id + " has a value of [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = vars[id][field];
                _push(value);

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void _setVar()
        {
            byte id = _pop();

            byte typeId = varTypes[id];

            byte numFields = types[typeId];

            Console.Write("var_" + id + " = [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = _pop();
                vars[id][field] = value;

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
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

        public byte Peek()
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
