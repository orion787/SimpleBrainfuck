using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static Stack<int> Stack = new Stack<int>();
		public static Dictionary<int, int> OpeningBracket = new Dictionary<int, int>();
        public static Dictionary<int, int> ClosingBracket = new Dictionary<int, int>();

		public static void Loop(IVirtualMachine vm)
		{
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                var bracket = vm.Instructions[i];
                switch (bracket)
                {
                    case '[':
                        Stack.Push(i);
                        break;
                    case ']':
                        ClosingBracket[i] = Stack.Peek();
                        OpeningBracket[Stack.Pop()] = i;
                        break;
                }
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
		{
            Loop(vm);

            vm.RegisterCommand('[', b => 
            {
                if (vm.Memory[vm.MemoryPointer] == 0)
                {
                    vm.InstructionPointer = OpeningBracket[vm.InstructionPointer];
                }
            });
            vm.RegisterCommand(']', b => 
            {
                if (vm.Memory[vm.MemoryPointer] != 0)
                {
                    vm.InstructionPointer = ClosingBracket[vm.InstructionPointer];
                }
            });
        }
	}
}