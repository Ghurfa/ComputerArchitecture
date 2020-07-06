using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCalculator
{
    public class Calculator
    {
        Stack<object> stack;
        public int Count => stack.Count;
        public Calculator()
        {
            stack.Clear();
        }
        public void Add(object item)
        {
            if (!(item.GetType() == typeof(int) || item.GetType() == typeof(Func<int, int, int>))) throw new ArgumentException();
            stack.Push(item);
        }
        public bool Pop()
        {
            if (Count == 0) return false;
            stack.Pop();
            return true;
        }
        public object Peek()
        {
            return stack.Peek();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            string command = "";
            while (true)
            {
                command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "push":
                        Console.WriteLine("Item to push?");
                        uint itemToPush = Convert.ToUInt32(Console.ReadLine());
                        calculator.Add(itemToPush);
                        Console.WriteLine($"Pushed {itemToPush}");
                        break;
                    case "peek":
                        Console.WriteLine($"Peek: {calculator.Peek()}");
                        break;
                    case "count":
                        Console.WriteLine(calculator.Count);
                        break;
                }
            }
        }
    }
}
