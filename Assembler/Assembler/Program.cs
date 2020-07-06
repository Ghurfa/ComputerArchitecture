using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
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
    class Program
    {
        static string[] Compact(string[] input)
        {
            int numOfUselessLines = 0;
            for (int i = 0; i < input.Length; i++)
            {
                input[i].Replace("  ", " ");
                string temp = input[i].Replace(" ", "").Replace("\t", "");
                if (temp.Length < 3)
                {
                    input[i] = "";
                    numOfUselessLines++;
                }
                else if (temp.Substring(0, 2) == "//")
                {
                    input[i] = "";
                    numOfUselessLines++;
                }
            }
            string[] ret = new string[input.Length - numOfUselessLines];
            int r = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Length < 3) continue;
                ret[r] = input[i];
                r++;
            }
            return ret;
        }
        static Dictionary<string, int> GetLabelDictionary(string[] input)
        {
            var labels = new Dictionary<string, int>();
            int lineNum = 0;
            foreach (string line in input)
            {
                if (line.First() == '[')
                {
                    labels.Add(line.Substring(1, line.Length - 2), lineNum);
                }
                else lineNum++;
            }
            return labels;
        }
        static void ReplaceOperators(string[] input)
        {
            string[] ops = Enum.GetNames(typeof(OpCodes));
            for (int i = 0; i < input.Length; i++)
            {
                foreach (string op in ops)
                {
                    int code = (int)Enum.Parse(typeof(OpCodes), op);
                    if (op.Length == 3 && input[i].Substring(0, 3) == op)
                    {
                        input[i] = code.ToString("X") + input[i].Substring(3);
                    }
                    else if (op.Length == 4 && input[i].Substring(0, 4) == op)
                    {
                        input[i] = code.ToString("X") + input[i].Substring(4);
                    }
                }
            }
        }
        static void Convert000(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Substring(0, 2) == "00") input[i] = input[i] + "000000";
            }
        }
        static void ConvertRRR(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].First() == '[') continue;
                int code = Convert.ToInt32(input[i].Substring(0, 2));
                if ((code >= 10 && code <= 22) ||
                   (code >= 24 && code <= 34))
                {
                    if (input[i][5] == ' ')
                    {
                        if (input[i][3] == 'r') input[i] = code + " 0" + input[i].Substring(4);
                    }
                    else input[i] = code + " " + input[i].Substring(4);
                    if (input[i][8] == ' ')
                    {
                        if (input[i][6] == 'r') input[i] = input[i].Substring(0, 6) + "0" + input[i].Substring(7);
                    }
                    else input[i] = code + " " + input[i].Substring(7);
                    if (input[i].Length == 11)
                    {
                        if (input[i][9] == 'r') input[i] = input[i].Substring(0, 9) + "0" + input[i][10];
                    }
                    else input[i] = code + " " + input[i].Substring(10);
                }
            }
        }
        static void Convert0RR(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].First() == '[') continue;
                int code = Convert.ToInt32(input[i].Substring(0, 2));
                if (code == 23 || code == 41)
                {
                    if (input[i][5] == ' ')
                    {
                        if (input[i][3] == 'r') input[i] = code + " 0" + input[i].Substring(4);
                    }
                    else input[i] = code + " " + input[i].Substring(4);
                    if (input[i].Length == 8)
                    {
                        if (input[i][6] == 'r') input[i] = input[i].Substring(0, 6) + "0" + input[i].Substring(7);
                    }
                    else input[i] = code + " " + input[i].Substring(7);
                    input[i] = code + " 00" + input[i].Substring(2);
                }
            }
        }
        static void ConvertRC(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].First() == '[') continue;
                int code = Convert.ToInt32(input[i].Substring(0, 2));
                if (code == 40 || code == 42 || code == 43 || code == 46 || code == 47 || code == 62)
                {
                    if (input[i][5] == ' ')
                    {
                        if (input[i][3] == 'r') input[i] = code + " 0" + input[i].Substring(4);
                    }
                    else input[i] = code + " " + input[i].Substring(4);
                    input[i] = input[i].Substring(0, 6) + input[i].Substring(6).PadLeft(4, '0');
                    input[i] = input[i].Substring(0, 8) + " " + input[i].Substring(8); 
                }
            }
        }
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\RockPaperScissors.txt");
            lines = Compact(lines);
            ReplaceOperators(lines);
            Convert000(lines);
            ConvertRRR(lines);
            Convert0RR(lines);
            ConvertRC(lines);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}
