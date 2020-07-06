using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CompArchLibrary
{
    public class EmulatorProgram
    {
        private Memory<ushort> program;
        public EmulatorProgram(Memory<ushort> program)
        {
            this.program = program;
        }
    }
}
