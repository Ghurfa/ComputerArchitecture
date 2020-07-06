using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompArchLibrary
{
    public class InstructionAssembler
    {
        static Dictionary<string, int> Compact(ref string[] input)
        {
            int numOfUselessLines = 0;
            for (int i = 0; i < input.Length; i++)
            {
                input[i].Replace("  ", " ");
                string temp = input[i].Replace(" ", "");
                if (temp.Length == 0)
                {
                    input[i] = "";
                    numOfUselessLines++;
                }
                else if (temp.Last() == ':' && temp.ToLower() != "progmem:") numOfUselessLines++;
                else if (temp.Substring(0, 2) == "//")
                {
                    input[i] = "";
                    numOfUselessLines++;
                }
            }
            var labels = new Dictionary<string, int>();
            string[] ret = new string[input.Length - numOfUselessLines];
            int r = 0;
            int pointer = 0;
            bool reachedProgMem = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Length == 0) continue;
                if(input[i].Replace(" ", "").ToLower() == "progmem:") reachedProgMem = true;
                else if (input[i].Replace(" ", "").Last() == ':')
                {
                    if(!reachedProgMem)
                    {
                        labels.Add(input[i].Split(':')[0], pointer);
                    }
                    continue;
                }
                ret[r] = input[i];
                r++;
                pointer += 2;
            }
            input = ret;
            return labels;
        }
        static Dictionary<string, int> SeparateProgMem(ref string[] input, out byte[] progMemOutput)
        {
            int i;
            for (i = 0; i < input.Length && input[i].ToLower() != "progmem:"; i++) ;
            LinkedList<byte>[] entries = new LinkedList<byte>[input.Length - i];
            for (int j = 0; j < entries.Length; j++)
                entries[j] = new LinkedList<byte>();
            Dictionary<string, int> progMemLabels = new Dictionary<string, int>();
            int pointer = i * 2;
            for (int j = i; j < input.Length; j++)
            {
                if (input[j].ToLower() == "progmem:")
                {
                    for (int k = 0; k < 4; k++)
                        entries[0].AddLast(0xFF);
                    pointer += 2;
                }
                else
                {
                    string[] parts = input[j].Split(new char[] { ':' }, 2);
                    progMemLabels.Add(parts[0], pointer);
                    int k;
                    for (k = 0; k < parts[1].Length && parts[1][k] == ' '; k++) ;
                    if (parts[1][k] == '"')
                    {
                        int temp = k;
                        for (k++; parts[1][k] != '"'; k++)
                            entries[j - i].AddLast((byte)(parts[1][k]));
                        entries[j - i].AddLast((byte)('\0'));
                        if ((k - temp) % 2 == 1)
                        {
                            entries[j - i].AddLast(0x00);
                            k++;
                        }
                        pointer += (k - temp) / 2;
                    }
                    else
                    {
                        string[] nums = parts[1].Replace(" ", "").Split(',');
                        if (nums.Length == 1)
                        {
                            ushort number = ushort.Parse(parts[1], System.Globalization.NumberStyles.HexNumber);
                            entries[j - i].AddLast((byte)(number & 0xFF));
                            entries[j - i].AddLast((byte)(number >> 8));
                            pointer++;
                        }
                        else if (nums.Length > 1)
                        {
                            ushort length = (ushort)nums.Length;
                            entries[j - i].AddLast((byte)(length >> 8));
                            entries[j - i].AddLast((byte)(length & 0xFF));
                            pointer++;
                            for (int l = 0; l < nums.Length; l++)
                            {
                                ushort number = ushort.Parse(nums[l], System.Globalization.NumberStyles.HexNumber);
                                entries[j - i].AddLast((byte)(number & 0xFF));
                                entries[j - i].AddLast((byte)(number >> 8));
                                pointer++;
                            }
                        }
                    }
                }
            }
            progMemOutput = new byte[2 * (pointer - 2 * i)];
            int index = 0;
            foreach (LinkedList<byte> linkedList in entries)
            {
                foreach (byte info in linkedList)
                {
                    progMemOutput[index] = info;
                    index++;
                }
            }
            if (i < input.Length)
            {
                string[] temp = new string[i];
                for (int j = 0; j < i; j++)
                {
                    temp[j] = input[j];
                }
                input = temp;
            }
            return progMemLabels;
        }
        static void ReplaceOperators(ref string[] input, ref byte[] output)
        {
            string[] ops = Enum.GetNames(typeof(OpCodes));
            for (int i = 0; i < input.Length; i++)
            {
                foreach (string op in ops)
                {
                    int code = (int)Enum.Parse(typeof(OpCodes), op);
                    if (op.Length == 3 && input[i].Substring(0, 3) == op)
                    {
                        if (code == 0) break;
                        output[i * 4] = (byte)code;
                        input[i] = input[i].Substring(4);
                        break;
                    }
                    else if (op.Length == 4 && input[i].Substring(0, 4) == op)
                    {
                        output[i * 4] = (byte)code;
                        input[i] = input[i].Substring(5);
                        break;
                    }
                }
            }
        }
        static void ConvertRegisters(ref string[] input, byte[] bytes)
        {
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length && input[i][j] != '[' && !(bytes[i * 4] == 0x70 && j > 1); j++)
                    if (input[i][j] == 'r') input[i] = input[i].Substring(0, j) + input[i].Substring(j + 1);
        }
        public static byte[] Assemble(string[] lines)
        {
            Dictionary<string, int> labels = Compact(ref lines);
            byte[] progMem;
            Dictionary<string, int> progMemLabels = SeparateProgMem(ref lines, out progMem);
            byte[] bytes = new byte[lines.Length * 4 + progMem.Length];
            for (int i = 0; i < progMem.Length; i++)
                bytes[i + (lines.Length * 4)] = progMem[i];
            ReplaceOperators(ref lines, ref bytes);
            ConvertRegisters(ref lines, bytes);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parameters = lines[i].Split(' ');
                int value;
                switch (bytes[i * 4])
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
                        bytes[i * 4 + 1] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = byte.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 3] = byte.Parse(parameters[2], System.Globalization.NumberStyles.HexNumber);
                        break;
                    case 0x23:
                    case 0x41:
                    case 0x44:
                    case 0x45:
                    case 0x54:
                    case 0x55:
                        bytes[i * 4 + 2] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 3] = byte.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber);
                        break;
                    case 0x40:
                        bytes[i * 4 + 1] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        if (parameters[1].First() == '[') value = labels[parameters[1].Substring(1, parameters[1].Length - 2)];
                        else value = int.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = (byte)(value >> 8);
                        bytes[i * 4 + 3] = (byte)value;
                        break;
                    case 0x42:
                    case 0x43:
                    case 0x46:
                    case 0x47:
                    case 0x62:
                        bytes[i * 4 + 1] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = (byte)(int.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber) >> 8);
                        bytes[i * 4 + 3] = (byte)int.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber);
                        break;
                    case 0x50:
                        if (lines[i].First() == '[') value = labels[lines[i].Substring(1, lines[i].Length - 2)];
                        else value = int.Parse(lines[i], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = (byte)((value >> 8) & 0xFF);
                        bytes[i * 4 + 3] = (byte)(value & 0xFF);
                        break;
                    case 0x51:
                    case 0x52:
                        bytes[i * 4 + 1] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        if (parameters[1].First() == '[') value = labels[parameters[1].Substring(1, parameters[1].Length - 2)];
                        else value = int.Parse(parameters[1], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = (byte)((value >> 8) & 0xFF);
                        bytes[i * 4 + 3] = (byte)(value & 0xFF);
                        break;
                    case 0x53:
                    case 0x60:
                    case 0x61:
                    case 0x71:
                    case 0x72:
                        bytes[i * 4 + 2] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        break;
                    case 0x56:
                        bytes[i * 4 + 2] = (byte)((int.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber) >> 8) & 0xFF);
                        bytes[i * 4 + 3] = (byte)(int.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber) & 0xFF);
                        break;
                    case 0x70:
                        bytes[i * 4 + 1] = byte.Parse(parameters[0], System.Globalization.NumberStyles.HexNumber);
                        bytes[i * 4 + 2] = (byte)((progMemLabels[parameters[1]] >> 8) & 0xFF);
                        bytes[i * 4 + 3] = (byte)(progMemLabels[parameters[1]] & 0xFF);
                        break;
                    default:
                        throw new Exception();
                }
            }
            return bytes;
        }
    }
}
