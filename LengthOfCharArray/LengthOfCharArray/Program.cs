using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LengthOfCharArray
{
    unsafe class Program
    {
        static int lengthOfCharArray(char* character)
        {
            int length = 0;
            startOfLoop:
            if (*(character + length) == '\0') return length;
            length++;
            goto startOfLoop;
        }
        static void Main(string[] args)
        {
            char[] first = "abcdefghi\0".ToCharArray();
            char[] second = "abcdefg\0hi\0".ToCharArray();
            fixed (char* ptr = first)
            {
                Console.WriteLine(lengthOfCharArray(ptr));
            }
            fixed (char* ptr = second)
            {
                Console.WriteLine(lengthOfCharArray(ptr));
            }
            Console.ReadLine();
        }
    }
}
