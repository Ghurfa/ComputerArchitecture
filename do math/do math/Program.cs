using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace do_math
{
    class Program
    {
        static byte DoMath(uint data)
        {
            byte instruction = (byte)(data >> 24);
            byte firstNum = (byte)((data >> 8) & 255);
            byte secondNum = (byte)(data & 255);

            switch (instruction)
            {
                case 1:
                    return (byte)(firstNum + secondNum);
                case 2:
                    return (byte)(firstNum - secondNum);
                case 3:
                    return (byte)(firstNum * secondNum);
                case 4:
                    return (byte)(firstNum / secondNum);
                case 5:
                    return (byte)(firstNum & secondNum);
                case 6:
                    return (byte)(firstNum | secondNum);
                case 7:
                    return (byte)(firstNum ^ secondNum);
                case 8:
                    return (byte)(firstNum << secondNum);
                case 9:
                    return (byte)(firstNum >> secondNum);
                default:
                    throw new InvalidOperationException();
            }
        }
        static void Main(string[] args)
        {
            //uint data = Convert.ToUInt32(Console.ReadLine());
            uint data = 0b00000100_00000000_10000000_00000100;
            Console.WriteLine(DoMath(data));
            Console.ReadKey();
        }
    }
}
