using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompArchLibrary;

namespace UnitTests
{
    [TestClass]
    public class AssemblerUnitTests
    {
        [TestMethod]
        public void TestRRR()
        {
            byte[] operators = { 0x10, 0x11, 0x12, 0x13, 0x14, 0x20, 0x21, 0x22, 0x24, 0x25, 0x30, 0x31, 0x32, 0x33, 0x34 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + " r0 r1 r2" };
                byte[] output = InstructionAssembler.Assemble(input);
                byte[] expectedOutput = new byte[] { op, 0x00, 0x01, 0x02 };
                CollectionAssert.AreEqual(expectedOutput, output, $"{opName} Failed");
            }
        }
        [TestMethod]
        public void Test0RR()
        {
            byte[] operators = { 0x23, 0x41, 0x44, 0x45, 0x54, 0x55 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + " r0 r1" };
                byte[] output = InstructionAssembler.Assemble(input);
                byte[] expectedOutput = new byte[] { op, 0x00, 0x00, 0x01 };
                CollectionAssert.AreEqual(expectedOutput, output, $"{opName} Failed");
            }
        }
        [TestMethod]
        public void TestRC()
        {
            byte[] operators = { 0x40, 0x42, 0x43, 0x46, 0x47, 0x51, 0x52, 0x62 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + " r1 1001" };
                byte[] output = InstructionAssembler.Assemble(input);
                byte[] expectedOutput = new byte[] { op, 0x01, 0x10, 0x01 };
                CollectionAssert.AreEqual(expectedOutput, output, $"{opName} Failed");
            }
        }
        [TestMethod]
        public void TestRCWithLabelThatExists()
        {
            byte[] operators = { 0x40, 0x51, 0x52 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + " r1 [label]", "label:" };
                byte[] output = InstructionAssembler.Assemble(input);
                byte[] expectedOutput = new byte[] { op, 0x01, 0x00, 0x02 };
                CollectionAssert.AreEqual(expectedOutput, output, $"{opName} Failed");
            }
        }
        [TestMethod]
        public void TestRCWithLabelThatDoesNotExist()
        {
            byte[] operators = { 0x40, 0x51, 0x52 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + " r1 [fakeLabel]", "label:" };
                Assert.ThrowsException<System.Collections.Generic.KeyNotFoundException>(() => { InstructionAssembler.Assemble(input); });
            }
        }
        [TestMethod]
        public void TestJump()
        {
            string[] input = new string[] { "jump 27" };
            byte[] output = InstructionAssembler.Assemble(input);
            byte[] expectedOutput = new byte[] { 0x50, 0x00, 0x00, 0x27 };
            CollectionAssert.AreEqual(expectedOutput, output, "Failed with line num");

            input = new string[] { "jump [label]", "label:" };
            output = InstructionAssembler.Assemble(input);
            expectedOutput = new byte[] { 0x50, 0x00, 0x00, 0x02 };
            CollectionAssert.AreEqual(expectedOutput, output, "Failed with label");
        }   
        [TestMethod]
        public void Test0R0()
        {
            byte[] operators = { 0x53, 0x60, 0x61, 0x71, 0x72 };
            foreach (byte op in operators)
            {
                string opName = Enum.GetName(typeof(OpCodes), op);
                string[] input = new string[] { opName + "r1" };
                byte[] output = InstructionAssembler.Assemble(input);
                byte[] expectedOutput = new byte[] { op, 0x00, 0x01, 0x00 };
                CollectionAssert.AreEqual(expectedOutput, output, $"{opName} Failed");
            }
        }
        [TestMethod]
        public void TestReturn()
        {
            string[] input = new string[] { "ret 3" };
            byte[] output = InstructionAssembler.Assemble(input);
            byte[] expectedOutput = new byte[] { 0x56, 0x00, 0x00, 0x03 };
            CollectionAssert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestLoadProgWithLabelThatExists()
        {
            string[] input = new string[] { "lprg r1 data", "progmem:", "data: 10" };
            byte[] output = InstructionAssembler.Assemble(input);
            byte[] expectedOutput = new byte[] { 0x70, 0x01, 0x00, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0x10, 0x00 };
            CollectionAssert.AreEqual(expectedOutput, output);
        }
        [TestMethod]
        public void TestLoadProgWithLabelThatDoesNotExist()
        {
            string[] input = new string[] { "lprg r1 fakeLabel", "progmem:", "data: 10" };
            Assert.ThrowsException<System.Collections.Generic.KeyNotFoundException>(() => { InstructionAssembler.Assemble(input); });
        }
        [TestMethod]
        public void JumpToProgMemLabel()
        {
            string[] input = new string[] { "jump [label]", "progmem:", "label: " };
            Assert.ThrowsException<System.Collections.Generic.KeyNotFoundException>(() => { InstructionAssembler.Assemble(input); });
        }
    }
}
