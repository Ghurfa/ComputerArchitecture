using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompArchLibrary;
namespace Disassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = File.ReadAllBytes(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\EmulatorTest.bin");
            int programLength;
            for (programLength = 0; programLength < bytes.Length * 4 && !(bytes[programLength * 4] == 0xFF); programLength++) ;
            string[] lines = new string[programLength];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = InstructionDisassembler.Disassemble(new byte[] { bytes[i * 4], bytes[i * 4 + 1], bytes[i * 4 + 2], bytes[i * 4 + 3] });
            }
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            for (int i = programLength * 4; i < bytes.Length; i++)
            {
                Console.Write(bytes[i].ToString("X").PadLeft(2, '0') + " ");
            }
            Console.ReadLine();
        }
    }
}
