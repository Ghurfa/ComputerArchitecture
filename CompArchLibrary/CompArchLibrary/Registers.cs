using System;
using System.Collections.Generic;
using System.Text;

namespace CompArchLibrary
{
    public class Registers
    {
        public ushort StackPointer;
        public ushort InstructionPointer;
        private ushort[] registers;
        public Registers(ushort instructionPointer, ushort stackPointer)
        {
            registers = new ushort[0x20];
            StackPointer = stackPointer;
            InstructionPointer = instructionPointer;
        }
        public ushort this[int index]
        {
            get => registers[index];
            set => registers[index] = value;
        }
    }
}
