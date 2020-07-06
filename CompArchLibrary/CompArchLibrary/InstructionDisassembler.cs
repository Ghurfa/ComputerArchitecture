using System;
using System.Collections.Generic;
using System.Text;

namespace CompArchLibrary
{
    public class InstructionDisassembler
    {
        public static string Disassemble(byte[] bytes)
        {
            string ret = Enum.GetName(typeof(OpCodes), (OpCodes)(bytes[0]));
            switch (bytes[0])
            {
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x14:
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x24:
                case 0x25:
                case 0x30:
                case 0x31:
                case 0x32:
                case 0x33:
                case 0x34:
                    ret += " r" + bytes[1].ToString("X") + " r" + bytes[2].ToString("X").PadLeft(2, '0') + " r" + bytes[3].ToString("X").PadLeft(2, '0');
                    break;
                case 0x23:
                case 0x41:
                case 0x44:
                case 0x45:
                case 0x54:
                case 0x55:
                    ret += " r" + bytes[2].ToString("X").PadLeft(2, '0') + " r" + bytes[3].ToString("X").PadLeft(2, '0');
                    break;
                case 0x40:
                case 0x42:
                case 0x43:
                case 0x46:
                case 0x47:
                case 0x62:
                case 0x70:
                    ret += " r" + bytes[1].ToString("X").PadLeft(2, '0') + " " + bytes[2].ToString("X").PadLeft(2, '0') + bytes[3].ToString("X").PadLeft(2, '0');
                    break;
                case 0x50:
                case 0x56:
                    ret += " " + bytes[2].ToString("X").PadLeft(2, '0') + bytes[3].ToString("X").PadLeft(2, '0');
                    break;
                case 0x51:
                case 0x52:
                    ret += " r" + bytes[1].ToString("X").PadLeft(2, '0') + " " + bytes[2].ToString("X").PadLeft(2, '0') + bytes[3].ToString("X").PadLeft(2, '0');
                    break;
                case 0x53:
                case 0x60:
                case 0x61:
                case 0x71:
                case 0x72:
                    ret += " r" + bytes[2].ToString("X").PadLeft(2, '0');
                    break;
            }
            return ret;
        }
        public static string Disassemble(uint data)
        {
            return Disassemble(new byte[] { (byte)(data >> 24), (byte)((data >> 16) & 0xFF), (byte)((data >> 8) & 0xFF), (byte)(data & 0xFF) });
        }
    }
}
