using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class MemoryStack
    {
        private Memory<ushort> stack;
        public MemoryStack(Memory<ushort> stackSpace)
        {
            stack = stackSpace;
        }
        public ushort this[int index]
        {
            get => stack.Span[index];
            set => stack.Span[index] = value;
        }
    }
}
