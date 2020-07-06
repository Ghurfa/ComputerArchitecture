using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public class BaseClass
    {
        public Func<int> GetRandomNumber;
        public BaseClass()
        {
            GetRandomNumber = () => 4;
        }
    }
    class InheritenceClass : BaseClass
    {
        public InheritenceClass()
        {
            GetRandomNumber = () => 6;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BaseClass thing = new BaseClass();
            Console.WriteLine(thing.GetRandomNumber());
            thing = new InheritenceClass();
            Console.WriteLine(thing.GetRandomNumber());
            Console.ReadLine();
        }
    }
}
