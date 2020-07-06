using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Emulator
{
    class MemoryMap
    {
        private ushort[] memory = new ushort[0x10000];
        private const int programStartIndex = 0x8000;
        private MemoryMappedIO memoryMappedIO;
        private EmulatorProgram program;
        private MemoryStack stack;
        public MemoryMap(ReadOnlySpan<byte> programData)
        {
            Span<ushort> programSpace = memory.AsSpan().Slice(programStartIndex);
            Span<byte> programSpaceBytes = MemoryMarshal.AsBytes(programSpace);
            programData.CopyTo(programSpaceBytes);
            memoryMappedIO = new MemoryMappedIO(memory.AsMemory(0, 0x100));
            program = new EmulatorProgram(memory.AsMemory(0x8000));
            stack = new MemoryStack(memory.AsMemory(0x4000, 0x7FFF));
        }

        public ushort this[int index]
        {
            get
            {
                if (index < 0x100) return memoryMappedIO[index];
                return memory[index];
            }
            set
            {
                if (index < 0x100) memoryMappedIO[index] = value;
                memory[index] = value;
            }
        }

        public MemoryMappedIO GetMemoryMappedIO() => memoryMappedIO;

        public EmulatorProgram GetProgram() => program;

        public MemoryStack GetStack() => stack;

        public ReadOnlySpan<byte> ProgramSpace => MemoryMarshal.AsBytes(memory.AsSpan().Slice(programStartIndex));

    }
}