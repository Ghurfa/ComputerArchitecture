using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace use_gotos
{
    class Program
    {
        static void First()
        {
            int i = 0;
            startOfLoop:
            Console.WriteLine(i);
            i++;
            if (i < 10) goto startOfLoop;
        }
        static void Second()
        {
            int i = -10;
            startOfLoop:
            if (!(i < 10)) goto endOfLoop;
            Console.WriteLine(i);
            i++;
            goto startOfLoop;
            endOfLoop:
            Console.WriteLine(i == 10);
        }
        static void Main(string[] args)
        {
            

           

            Console.ReadLine();
        }
    }
}
