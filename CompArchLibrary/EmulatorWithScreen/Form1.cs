using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompArchLibrary;

namespace EmulatorWithScreen
{
    public partial class Form1 : Form
    {
        private MemoryMap memoryMap;
        private Registers registers;
        private EmulatorProgram program;
        private RegistersView registerDebugWindow;
        private MMIOView mmioDebugWindow;
        private static Color getPixelColor(ushort data) => Color.FromArgb((data & 0b11111) * 8, ((data >> 5) & 0b11111) * 8, ((data >> 10) & 0b11111) * 8);
        public Form1()
        {
            InitializeComponent();
            instructionList.AutoScroll = true;
            byte[] bytes = File.ReadAllBytes(@"C:\Users\VisitorDL11\Documents\LorenzoLopezComputerArchitecture\EmulatorTest.bin");

            memoryMap = new MemoryMap(bytes, (index, value, mmio) =>
            {
                mmio.Span[index] = value;
                Control mmioDebugLabel = mmioDebugWindow.Controls.Find("mmioDebugLabel", true)[0];
                string newLine = index.ToString("X").PadLeft(4, '0') + ": " + value.ToString("X").PadLeft(4, '0') + '\n';
                mmioDebugLabel.Text = mmioDebugLabel.Text.Substring(0, 11 * index) + newLine + mmioDebugLabel.Text.Substring(11 * (index + 1));
                if (index == 5 && (uint)(value) % 2 == 1)
                {
                    printedInstructions.Text += mmio.Span[4].ToString() + '\n';
                    mmio.Span[5] = 0x0000;
                    mmioDebugLabel.Text = mmioDebugLabel.Text.Substring(0, 61) + "0000\n" + mmioDebugLabel.Text.Substring(66);
                }
                else if (index == 7 && (uint)(value) % 2 == 1)
                {
                    printedInstructions.Text += (char)(mmio.Span[6]) + '\n';
                    mmio.Span[7] = 0x0000;
                    mmioDebugLabel.Text = mmioDebugLabel.Text.Substring(0, 83) + "0000\n" + mmioDebugLabel.Text.Substring(88);
                }
                else if(index >= 0xC0)
                {
                    Control pixel = Controls.Find("panel" + (index - 0xC0 + 1), false)[0];
                    pixel.BackColor = getPixelColor(value);
                }
            });
            registers = new Registers(memoryMap.ProgramStartIndex, memoryMap.StackStartIndex);

            program = memoryMap.GetProgram();
        }
        private void updateRegistersWindow()
        {
            string registerDebugString = "";
            for(int i = 0; i < 32; i++)
                registerDebugString += "r" + i.ToString("X").PadLeft(2, '0') + ":  " + registers[i].ToString("X").PadLeft(4, '0') + '\n';
            registerDebugWindow.Controls.Find("registersDebugLabel", false)[0].Text = registerDebugString;
        }
        private void inputCharButton_Click(object sender, EventArgs e)
        {
            if (inputCharTextbox.Text.Length == 0) return;
            memoryMap[2] = inputCharTextbox.Text[0];
            memoryMap[3] = 0xFFFF;
        }

        private void inputNumButton_Click(object sender, EventArgs e)
        {
            memoryMap[2] = (ushort)inputNumTextbox.Value;
            memoryMap[3] = 0xFFFF;
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            //endian stuff
            byte firstByte = (byte)(memoryMap[registers.InstructionPointer] & 0xFF);
            byte secondByte = (byte)((memoryMap[registers.InstructionPointer] >> 8) & 0xFF);
            byte thirdByte = (byte)(memoryMap[registers.InstructionPointer + 1] & 0xFF);
            byte fourthByte = (byte)((memoryMap[registers.InstructionPointer + 1] >> 8) & 0xFF);
            uint instructionData = (uint)((firstByte << 24) + (secondByte << 16) + (thirdByte << 8) + fourthByte);
            Instruction instruction = new Instruction(instructionData, (str) =>
            {
                printedInstructions.Text += str;
            },
            () =>
            {
                printedInstructions.Text += '\n';
            });
            instruction.Execute(memoryMap, registers);
            updateRegistersWindow();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            registerDebugWindow = new RegistersView();
            registerDebugWindow.Show();
            updateRegistersWindow();
            mmioDebugWindow = new MMIOView();
            mmioDebugWindow.Show();
            Control mmioLabel = mmioDebugWindow.Controls.Find("mmioDebugLabel", true)[0];
            mmioLabel.Text = "";
            for (int i = 0; i < 0x100; i++)
                mmioLabel.Text += i.ToString("X").PadLeft(4, '0') + ": " + memoryMap[i].ToString("X").PadLeft(4, '0') + '\n';
        }
    }
}
