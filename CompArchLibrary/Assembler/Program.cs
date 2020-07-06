using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CompArchLibrary;

namespace Assembler
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\EmulatorTest.txt");
            byte[] bytes = InstructionAssembler.Assemble(lines);
            /*for(int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"{bytes[i * 4].ToString("X").PadLeft(2, '0')} {bytes[i * 4 + 1].ToString("X").PadLeft(2, '0')} {bytes[i * 4 + 2].ToString("X").PadLeft(2, '0')} {bytes[i * 4 + 3].ToString("X").PadLeft(2, '0')}");
            }
            Console.ReadLine();*/
            File.WriteAllBytes(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\EmulatorTest.bin", bytes);
        }
    }
}
