using CompArchLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompArchLibrary
{
    public class Instruction
    {
        private uint data;
        private Action<string> printString;
        private Action breakLine;
        public Instruction(uint data, Action<string> printInfo, Action breakLine)
        {
            this.data = data;
            printString = printInfo;
            this.breakLine = breakLine;
        }
        public void Execute(MemoryMap memoryMap, Registers registers)
        {
            string debugValues = InstructionDisassembler.Disassemble(data);
            printString(debugValues);
            breakLine();
            byte operation = (byte)(data >> 24);
            byte src;
            byte src1;
            byte src2;
            byte dest;
            byte reg;
            byte addrReg;
            ushort howMuch;
            ushort address;
            registers.InstructionPointer += 2;
            switch (operation)
            {
                case 0x10:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] + registers[src2]);
                    break;
                //Addition
                case 0x11:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] - registers[src2]);
                    break;
                //Subtraction
                case 0x12:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] * registers[src2]);
                    break;
                //Multiplication
                case 0x13:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] / registers[src2]);
                    break;
                //Division
                case 0x14:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] % registers[src2]);
                    break;
                //Modulo
                case 0x20:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] & registers[src2]);
                    break;
                //And
                case 0x21:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] | registers[src2]);
                    break;
                //Or
                case 0x22:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] ^ registers[src2]);
                    break;
                //Xor
                case 0x23:
                    src = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (ushort)~registers[src];
                    break;
                //Not
                case 0x24:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] << registers[src2]);
                    break;
                //Left Shift
                case 0x25:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = (byte)(registers[src1] >> registers[src2]);
                    break;
                //Right Shift
                case 0x30:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = src1 == src2 ? (ushort)0xFFFF : (ushort)0x0000;
                    break;
                //Equals
                case 0x31:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = src1 < src2 ? (ushort)0xFFFF : (ushort)0x0000;
                    break;
                //Less
                case 0x32:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = src1 > src2 ? (ushort)0xFFFF : (ushort)0x0000;
                    break;
                //Greater
                case 0x33:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = src1 <= src2 ? (ushort)0xFFFF : (ushort)0x0000;
                    break;
                //Less than or equal to
                case 0x34:
                    src1 = (byte)((data >> 16) & 0xFF);
                    src2 = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 255);
                    registers[dest] = src1 >= src2 ? (ushort)0xFFFF : (ushort)0x0000;
                    break;
                //Greater than or equal to
                case 0x40:
                    dest = (byte)((data >> 16) & 0xFF);
                    ushort value = (ushort)(data & 0xFFFF);
                    registers[dest] = value;
                    break;
                //Set
                case 0x41:
                    src = (byte)((data >> 8) & 0xFF);
                    dest = (byte)(data & 0xFF);
                    registers[dest] = registers[src];
                    break;
                //Copy
                case 0x42:
                    dest = (byte)((data >> 16) & 0xFF);
                    address = (ushort)(data & 0xFFFF);
                    registers[dest] = memoryMap[address];
                    break;
                //Load
                case 0x43:
                    src = (byte)((data >> 16) & 0xFF);
                    address = (ushort)(data & 0xFFFF);
                    memoryMap[address] = registers[src];
                    break;
                //Store
                case 0x44:
                    dest = (byte)((data >> 8) & 0xFF);
                    addrReg = (byte)(data & 0xFF);
                    address = registers[addrReg];
                    registers[dest] = memoryMap[address];
                    break;
                //Load Indirect
                case 0x45:
                    src = (byte)((data >> 8) & 0xFF);
                    addrReg = (byte)(data & 0xFF);
                    address = registers[addrReg];
                    memoryMap[address] = registers[src];
                    break;
                //Store Indirect
                case 0x46:
                    reg = (byte)((data >> 16) & 0xFF);
                    howMuch = (ushort)(data & 0xFFFF);
                    registers[reg] += howMuch;
                    break;
                //Increment
                case 0x47:
                    reg = (byte)((data >> 16) & 0xFF);
                    howMuch = (ushort)(data & 0xFFFF);
                    registers[reg] -= howMuch;
                    break;
                //Decrement
                case 0x50:
                    address = (ushort)((data & 0xFFFF) + memoryMap.ProgramStartIndex);
                    registers.InstructionPointer = address;
                    break;
                //Jump
                case 0x51:
                    src = (byte)((data >> 16) & 0xFF);
                    address = (ushort)((data & 0xFFFF) + memoryMap.ProgramStartIndex);
                    if ((registers[src] & 1) == 1) registers.InstructionPointer = address;
                    break;
                //Jump If
                case 0x52:
                    src = (byte)((data >> 16) & 0xFF);
                    address = (ushort)((data & 0xFFFF) + memoryMap.ProgramStartIndex);
                    if ((registers[src] & 1) == 0) registers.InstructionPointer = address;
                    break;
                //Jump If Not
                case 0x53:
                    addrReg = (byte)((data >> 8) & 0xFF);
                    address = (ushort)(registers[addrReg] + memoryMap.ProgramStartIndex);
                    registers.InstructionPointer = address;
                    break;
                //Jump Indirect
                case 0x54:
                    addrReg = (byte)(data & 0xFF);
                    address = (ushort)(registers[addrReg] + memoryMap.ProgramStartIndex);
                    src = (byte)((data >> 8) & 0xFF);
                    if ((registers[src] & 1) == 1) registers.InstructionPointer = address;
                    else break;
                    break;
                //Jump If Indirect
                case 0x55:
                    addrReg = (byte)(data & 0xFF);
                    address = (ushort)(registers[addrReg] + memoryMap.ProgramStartIndex);
                    src = (byte)((data >> 8) & 0xFF);
                    if ((registers[src] & 1) == 0) registers.InstructionPointer = address;
                    else break;
                    break;
                //Jump If Indirect
                case 0x56:
                    ushort numOfParameters = (ushort)(data & 0xFFFF);
                    registers.StackPointer--;
                    registers[0xFF] = memoryMap.GetStack()[registers.StackPointer];
                    registers.StackPointer -= numOfParameters;
                    registers.InstructionPointer = registers[0xFF];
                    break;
                //Return
                case 0x60:
                    src = (byte)((data >> 8) & 0xFF);
                    memoryMap.GetStack()[registers.StackPointer] = registers[src];
                    registers.StackPointer++;
                    break;
                //Push
                case 0x61:
                    dest = (byte)((data >> 8) & 0xFF);
                    registers.StackPointer--;
                    registers[dest] = memoryMap.GetStack()[registers.StackPointer];
                    break;
                //Pop
                case 0x62:
                    dest = (byte)((data >> 8) & 0xFF);
                    registers[dest] = memoryMap.GetStack()[(registers.StackPointer - 1)];
                    break;
                //Peek
                case 0x70:
                    dest = (byte)((data >> 16) & 0xFF);
                    address = (ushort)(data & 0xFFFF);
                    registers[dest] = (ushort)(memoryMap.ProgramStartIndex + address);
                    break;
                case 0x71:
                    src = (byte)((data >> 8) & 0xFF);
                    address = registers[src];
                    for (int i = address * 2; ((memoryMap[i / 2] >> (8 * (i % 2))) & 255) != 0x00; i++)
                    {
                        printString(((char)((memoryMap[i / 2] >> (8 * (i % 2))) & 255)).ToString());
                    }
                    breakLine();
                    break;
                //Print String
                case 0x72:
                    src = (byte)((data >> 8) & 0xFF);
                    address = registers[src];
                    int arrayLength = memoryMap[address];
                    for(int i = 0; i < arrayLength; i++)
                    {
                        memoryMap[0xC0 + i] = memoryMap[address + i + 1];
                    }
                    break;
                //Draw image
                default:
                    throw new InvalidOperationException($"Unknown op code {operation.ToString("X")}");
            }
        }
    }
}
