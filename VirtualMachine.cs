using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        Dictionary<char, Action<IVirtualMachine>> history =
            new Dictionary<char, Action<IVirtualMachine>>();


        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            InstructionPointer = 0;
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            history[symbol] = execute;
        }

        public void Run()
        {
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
                var instruction = Instructions[InstructionPointer];
                if (history.ContainsKey(instruction))
                    history[instruction].Invoke(this);
            }
        }
    }
}