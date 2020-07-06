using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    public enum OpCodes
    {
        nop,
        add = 0x10,
        sub,
        mul,
        div,
        mod,
        and = 0x20,
        orr,
        xor,
        not,
        lsh,
        rsh,
        equl = 0x30,
        less,
        grtr,
        lseq,
        greq,
        sett = 0x40,
        copy,
        load,
        stor,
        lodi,
        stri,
        incr,
        decr,
        jump = 0x50,
        jmpf,
        jifn,
        jind,
        jifi,
        jfni,
        rtrn,
        push = 0x60,
        popp,
        peek
    }
    class Instruction
    {
        private uint data;
        public Instruction(uint data)
        {
            this.data = data;
        }
        public void Execute(MemoryMap memoryMap, Registers registers, Dictionary<byte, Func<MemoryMap, Registers, uint, bool>> operations)
        {
            Console.WriteLine($"Executing {data.ToString("X").PadRight(8)} \t {(OpCodes)(data >> 24)} \t {registers.InstructionPointer + 1}");
            byte operation = (byte)(data >> 24);
            Func<MemoryMap, Registers, uint, bool> function = operations[operation];
            bool didJump = function(memoryMap, registers, data);
            if (!didJump) registers.InstructionPointer++;
        }
    }
}
