using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pointers
{
    unsafe class Program
    {
        static int GetNthByte(uint input, int n)
        {
            byte* firstByte = (byte*)&input;
            byte* ret = firstByte + n;
            return *ret;
        }
        static void LoopThroughIntArray(int* firstPtr, int length)
        {
            int i = 0;
            startOfLoop:
            if (i == length) return;
            Console.WriteLine(*(firstPtr + i));
            i++;
            goto startOfLoop;
        }
        static void LoopThroughIntArray(int* firstPtr, int* endPtr)
        {
            startOfLoop:
            if (firstPtr == endPtr) return;
            Console.WriteLine(*firstPtr);
            firstPtr++;
            goto startOfLoop;
        }
        static void Main(string[] args)
        {
            /*uint input = 0x89_AB_23_10;
            Console.WriteLine(GetNthByte(input, 0).ToString("X"));
            Console.WriteLine(GetNthByte(input, 1).ToString("X"));
            Console.WriteLine(GetNthByte(input, 2).ToString("X"));
            Console.WriteLine(GetNthByte(input, 3).ToString("X"));*/

            int[] array = { 0, 1, 2, 5, 6, 7 };
            fixed(int* x = array)
            {                  
                LoopThroughIntArray(x, array.Length);
                Console.WriteLine();
                LoopThroughIntArray(x, x + array.Length);
            }
            Console.ReadLine();
        }
    }
}
