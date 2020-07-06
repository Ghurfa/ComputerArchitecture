using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Emulator
{
    class EmulatorProgram
    {
        private Memory<ushort> program;
        public EmulatorProgram(Memory<ushort> program)
        {
            this.program = program;
        }
        public Instruction GetInstruction(ushort index)
        {
            byte firstByte = (byte)(program.Span[index * 2] & 0xFF);
            byte secondByte = (byte)((program.Span[index * 2] >> 8) & 0xFF);
            byte thirdByte = (byte)(program.Span[index * 2 + 1] & 0xFF);
            byte fourthByte = (byte)((program.Span[index * 2 + 1] >> 8) & 0xFF);
            return new Instruction((uint)((firstByte << 24) + (secondByte << 16) + (thirdByte << 8) + fourthByte));
        }
    }
}
