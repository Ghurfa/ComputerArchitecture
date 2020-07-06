using System;
using System.Collections.Generic;
using System.Text;

namespace CompArchLibrary
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
        set = 0x40,
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
        ret,
        push = 0x60,
        pop,
        peek,
        lprg = 0x70,
        prts,
        dimg
    }
}
