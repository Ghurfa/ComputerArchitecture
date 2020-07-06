using System;
using System.Collections.Generic;
using System.Text;

namespace CompArchLibrary
{
    public class MemoryMappedIO
    {
        private Memory<ushort> mmio;
        private Action<int, ushort, Memory<ushort>> setAction;
        public MemoryMappedIO(Memory<ushort> mmio, Action<int, ushort, Memory<ushort>> setAction)
        {
            this.mmio = mmio;
            this.setAction = setAction;
            Random random = new Random();
            mmio.Span[0] = (ushort)random.Next();
        }
        public virtual ushort this[int index]
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
            set => setAction(index, value, mmio);
        }
    }
}
