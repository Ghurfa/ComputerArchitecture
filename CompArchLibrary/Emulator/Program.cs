using System;
using System.Collections.Generic;
using System.IO;
using CompArchLibrary;

namespace Emulator
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = File.ReadAllBytes(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\EmulatorTest.bin");

            MemoryMap memoryMap = new MemoryMap(bytes, (index, value, mmio) =>
            {
                mmio.Span[index] = value;
                if (index == 5 && (uint)(value) % 2 == 1)
                {
                    Console.WriteLine(mmio.Span[4]);
                    mmio.Span[5] = 0x0000;
                }
                else if (index == 7 && (uint)(value) % 2 == 1)
                {
                    Console.WriteLine((char)mmio.Span[6]);
                    mmio.Span[7] = 0x0000;
                }
            });
            Registers registers = new Registers((ushort)memoryMap.ProgramStartIndex, (ushort)memoryMap.StackStartIndex);

            EmulatorProgram program = memoryMap.GetProgram();

            while ((byte)(memoryMap[registers.InstructionPointer] & 0xFF) != 0xFF)
            {
                while (!Console.KeyAvailable) { }
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Spacebar)
                {
                    //endian stuff
                    byte firstByte = (byte)(memoryMap[registers.InstructionPointer] & 0xFF);
                    byte secondByte = (byte)((memoryMap[registers.InstructionPointer] >> 8) & 0xFF);
                    byte thirdByte = (byte)(memoryMap[registers.InstructionPointer + 1] & 0xFF);
                    byte fourthByte = (byte)((memoryMap[registers.InstructionPointer + 1] >> 8) & 0xFF);
                    uint instructionData = (uint)((firstByte << 24) + (secondByte << 16) + (thirdByte << 8) + fourthByte);
                    Instruction instruction = new Instruction(instructionData, (str) =>
                    {
                        Console.Write(str);
                    }, () =>
                     {
                         Console.WriteLine();
                     });
                    instruction.Execute(memoryMap, registers);
                }
                else if (key.Key == ConsoleKey.T)
                {
                    Console.CursorLeft = 0;
                    Console.WriteLine("Enter key:");
                    memoryMap[2] = Console.ReadKey().KeyChar;
                    memoryMap[3] = 0xFFFF;
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.R)
                {
                    Console.CursorLeft = 0;
                    Console.WriteLine("Enter num:");
                    memoryMap[2] = ushort.Parse(Console.ReadLine());
                    memoryMap[3] = 0xFFFF;
                }
            }
            while (true) ;
        }
    }
}
