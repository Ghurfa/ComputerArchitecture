using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loop_Through_Array_again
{
    unsafe class Program
    {
        static void LoopThroughIntArray(int* array, int length)
        {
            int count = 0;
            startOfLoop:
            if (count >= length) return;
            Console.WriteLine(*array);
            count++;
            array++;
            goto startOfLoop;
        }
        static void Main(string[] args)
        {
            int[] array = { 0, 1, 2, 5, 7, 6 };
            fixed(int* x = array)
            {
                LoopThroughIntArray(x, array.Length);
            }
            Console.ReadLine();
        }
    }
}
