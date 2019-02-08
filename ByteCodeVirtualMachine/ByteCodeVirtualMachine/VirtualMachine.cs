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

                switch ((InstructionsEnum)instruction)
                {
                    case InstructionsEnum.Literal:
                        literal(bytes, ref i);
                        break;
                    case InstructionsEnum.Add: //should be preceded by two values to add
                        add();
                        break;
                    case InstructionsEnum.Subtract: //should be preceded by two values to add
                        subtract();
                        break;
                    case InstructionsEnum.Multiply:
                        multiply();
                        break;
                    case InstructionsEnum.Divide:
                        divide();
                        break;
                    case InstructionsEnum.If:
                        @if();
                        break;
                    case InstructionsEnum.EndIf:
                        endIf();
                        break;
                    case InstructionsEnum.For:
                        @for(i);
                        break;
                    case InstructionsEnum.EndFor:
                        endFor(ref i);
                        break;
                    case InstructionsEnum.DefArray:
                        defArray();
                        break;
                    case InstructionsEnum.GetArrayLength:
                        getArrayLength();
                        break;
                    case InstructionsEnum.GetArrayValueAtIndex:
                        getArrayValueAtIndex();
                        break;
                    case InstructionsEnum.SetArrayValueAtIndex:
                        setArrayValueAtIndex();
                        break;
                    case InstructionsEnum.DefType:
                        defType();
                        break;
                    case InstructionsEnum.DefVar:
                        defVar();
                        break;
                    case InstructionsEnum.GetVar:
                        getVar();
                        break;
                    case InstructionsEnum.SetVar:
                        setVar();
                        break;
                    case InstructionsEnum.GetStat: //should be preceded by value to tell us which stat to get
                        getStat();
                        break;
                    case InstructionsEnum.SetStat:
                        setStat();
                        break;
                }

                Console.WriteLine(this);
            }
        }

        private void literal(byte[] bytes, ref int i)
        {
            // Read the next byte from the bytecode.
            byte value = bytes[++i];

            //push to stack
            push(value);

            Console.WriteLine("Setting literal to " + value);
        }

        private void add()
        {
            byte right = pop();
            byte left = pop();

            push((byte)(left + right)); //assume we won't overflow

            Console.WriteLine("Adding " + right + " + " + left);
        }

        private void subtract()
        {
            byte right = pop();
            byte left = pop();

            push((byte)(left - right)); //assume we won't overflow
        }

        private void multiply()
        {
            byte right = pop();
            byte left = pop();

            push((byte)(left * right)); //assume we won't overflow
        }

        private void divide()
        {
            byte right = pop();
            byte left = pop();

            push((byte)(left / right)); //assume we won't overflow
        }

        private void @if()
        {
            byte value = pop();
            ifs.Push(value != 0);
        }

        private void endIf()
        {
            ifs.Pop();
        }

        private void @for(int i)
        {
            byte value = pop();

            indexesWhereForStarted.Push(i);
            forCounts.Push(value);

            Console.WriteLine("Loop " + value + " times.");
        }

        private void endFor(ref int i)
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

        private void defArray()
        {
            byte length = pop();
            byte id = pop();
            byte typeId = pop();

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

        private void getArrayLength()
        {
            byte id = pop();

            byte length = (byte)arrays[id].Length;

            Console.WriteLine("array_" + id + " has length of " + length);
            push(length);
        }

        private void getArrayValueAtIndex()
        {
            byte index = pop();
            byte id = pop();

            byte typeId = arrayTypes[id];

            byte numFields = types[typeId];

            Console.Write("array_" + id + "[" + index + "] has a value of [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = arrays[id][index][field];
                push(value);

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void setArrayValueAtIndex()
        {
            byte id = pop();
            byte index = pop();

            byte typeId = arrayTypes[id];

            byte numFields = types[typeId];

            Console.Write("array_" + id + "[" + index + "]" + " = [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = pop();
                arrays[id][index][field] = value;

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void defType()
        {
            byte numBytes = pop();
            byte id = pop();

            //push to arrays table
            types[id] = numBytes;

            Console.WriteLine("type_" + id + " = new type[" + numBytes + "]");
        }

        private void defVar()
        {
            byte id = pop();
            byte typeId = pop();

            varTypes[id] = typeId;

            byte numFields = types[typeId];

            vars[id] = new byte[numFields];

            Console.WriteLine("type_" + typeId + " var_" + id + " = new type_" + typeId + "()");
        }

        private void getVar()
        {
            byte id = pop();

            byte typeId = varTypes[id];

            byte numFields = types[typeId];

            Console.Write("var_" + id + " has a value of [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = vars[id][field];
                push(value);

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void setVar()
        {
            byte id = pop();

            byte typeId = varTypes[id];

            byte numFields = types[typeId];

            Console.Write("var_" + id + " = [");
            for (int field = 0; field < numFields; field++)
            {
                byte value = pop();
                vars[id][field] = value;

                if (field == numFields - 1)
                    Console.WriteLine(value + "]");
                else
                    Console.Write(value + ", ");
            }
        }

        private void getStat()
        {
            byte stat = pop();
            push(tempGetStatValue(stat));
        }

        private void setStat()
        {
            byte value = pop();
            byte stat = pop();

            tempSetStatValue(stat, value);
        }

        private void push(byte value)
        {
            //check for overflow
            if (_stackSize >= MAX_STACK_SIZE)
                throw new OverflowException("Cannot push to a full stack!");

            _stack[_stackSize++] = value;
        }

        private byte pop()
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

        private byte tempGetStatValue(byte stat)
        {
            return 1;
        }

        private void tempSetStatValue(byte stat, byte value)
        {
            //TOOD: set
        }
    }
}
