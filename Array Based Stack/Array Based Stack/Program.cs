using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Based_Stack
{
    public class ArrayBasedStack
    {
        public int Count { get; private set; }
        private uint[] array;
        public ArrayBasedStack(int size = 20)
        {
            Count = 0;
            array = new uint[size];
        }
        public void Push(uint value)
        {
            array[Count] = value;
            Count++;
        }
        public uint Pop()
        {
            uint ret = array[Count - 1];
            Count--;
            return ret;
        }
        public void PopMultipleValues(int howMany)
        {
            if (howMany > Count) throw new InvalidOperationException("Popping too many values");
            Count -= howMany;
        }
        public uint Peek(int indexFromTop = 0)
        {
            uint ret = array[Count - 1 - indexFromTop];
            return ret;
        }
    }
    public class Calculator
    {
        private ArrayBasedStack stack;
        public int Count => stack.Count;
        public Calculator(int size = 100)
        {
            stack = new ArrayBasedStack(size);
        }
        public void Push(uint value) => stack.Push(value);
        public uint Peek() => stack.Peek();
        public void Add()
        {
            uint upperValue = stack.Peek();
            stack.Pop();
            uint lowerValue = stack.Peek();
            stack.Pop();
            stack.Push(upperValue + lowerValue);
        }
        public void Subtract()
        {
            Not();
            Add();
            Not();
        }
        public void Multiply()//x(y-1) + x
        {
            /*uint upperValue = stack.Peek();
            stack.Pop();
            uint lowerValue = stack.Peek();
            stack.Pop();
            int mask = 1;
            uint sum = 0;
            int count = 0;
            startOfLoop:
            if (mask > lowerValue) goto endOfLoop;
            if ((mask & lowerValue) == mask) sum += upperValue << count;
            count++;
            mask <<= 1;
            goto startOfLoop;
            endOfLoop:
            stack.Push(sum);*/
            uint upperValue = stack.Peek();
            stack.Pop();
            uint lowerValue = stack.Peek();
            stack.Pop();
            if (lowerValue == 0) stack.Push(0);
            else
            {
                stack.Push(lowerValue - 1);
                stack.Push(upperValue);
                Multiply();
                stack.Push(upperValue);
                Add();
            }
        }
        public void Divide()//(x-y)/y + 1
        {
            uint upperValue = stack.Peek();
            stack.Pop();
            uint lowerValue = stack.Peek();
            stack.Pop();
            if (lowerValue == 0)
            {
                stack.Push(lowerValue);
                stack.Push(upperValue);
                throw new DivideByZeroException();
            }
            if (upperValue < lowerValue) stack.Push(0);
            else
            {
                stack.Push(lowerValue);
                stack.Push(lowerValue);
                stack.Push(upperValue);
                Subtract();
                Divide();
                stack.Push(1);
                Add();
            }
        }
        public void Not()
        {
            uint value = stack.Peek();
            stack.Pop();
            stack.Push(~value);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            string command = "";
            while(true)
            {
                command = Console.ReadLine();
                switch(command.ToLower())
                {
                    case "push":
                        Console.WriteLine("Item to push?");
                        uint itemToPush = Convert.ToUInt32(Console.ReadLine());
                        calculator.Push(itemToPush);
                        Console.WriteLine($"Pushed {itemToPush}");
                        break;
                    case "peek":
                        Console.WriteLine($"Peek: {calculator.Peek()}");
                        break;
                    case "add":
                        calculator.Add();
                        break;
                    case "subtract":
                        calculator.Subtract();
                        break;
                    case "multiply":
                        calculator.Multiply();
                        break;
                    case "divide":
                        calculator.Divide();
                        break;
                    case "count":
                        Console.WriteLine(calculator.Count);
                        break;
                }
            }
        }
    }
}
