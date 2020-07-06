using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bit_shifting_practice
{
    class Program
    {
        static int ClosestPowOf2(int num)
        {
            int temp = num;
            int i;
            for (i = 0; temp > 1; i++)
                temp >>= 1;
            for (; i > 0; i--)
                temp <<= 1;
            return num > temp + temp >> 1 ? temp << 1 : temp;
        }
        static bool IsPowOf2(int num)
        {
            int x = 1;
            while (x <= num)
            {
                if ((x & num) == x)
                {
                    if (x == num) return true;
                    return false;
                }
                x <<= 1;
            }
            return false;
        }
        static int FlipNthBit(int num, int n)
        {
            int mask = 1;
            for (; n > 0; n--)
                mask <<= 1;
            return num ^ mask;
        }
        static int ModByPowOf2(int num, int pow)
        {
            int mask = 1;
            for (; pow > 1; pow--)
                mask = (mask << 1) | 1;
            return num & mask;
        }
        static int GetNthByte(int num, int n)
        {
            int mask = 255;
            return (num >> (8 * n)) & mask;
            //return num & (mask << (8 * n   ))
        }
        static int MultiplyByPowOf2(int num, int pow) => num << pow;
        static int DivideByPowOf2(int num, int pow) => num >> pow;
        static void Main(string[] args)
        {
            int first;
            int second;
            int output;
            while (true)
            {
                first = Convert.ToInt32(Console.ReadLine());
                second = Convert.ToInt32(Console.ReadLine());
                output = GetNthByte(first, second);
                Console.WriteLine($"First: {Convert.ToString(first, 2)}");
                Console.WriteLine($"Second: {Convert.ToString(second, 2)}");
                Console.WriteLine($"Output: {output} = {Convert.ToString(output, 2)}");
            }
        }
    }
}
