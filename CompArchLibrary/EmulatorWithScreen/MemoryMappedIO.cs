using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class MemoryMappedIO
    {
        private Memory<ushort> mmio;
        public MemoryMappedIO(Memory<ushort> mmio)
        {
            this.mmio = mmio;
            Random random = new Random();
            mmio.Span[0] = (ushort)random.Next();
        }
        public ushort this[int index]
        {
            get
            {
                ushort ret = mmio.Span[index];
                if (index == 0)
                {
                    Random random = new Random();
                    mmio.Span[0] = (ushort)random.Next();
                }
                return ret;
            }
            set
            {
                mmio.Span[index] = value;
                if(index == 5 && (uint)(value) % 2 == 1)
                {
                    Console.WriteLine(mmio.Span[4]);
                    mmio.Span[5] = 0x0000;
                }
                else if (index == 7 && (uint)(value) % 2 == 1)
                {
                    Console.WriteLine((char)mmio.Span[6]);
                    mmio.Span[7] = 0x0000;
                }
            }
        }
    }
}
