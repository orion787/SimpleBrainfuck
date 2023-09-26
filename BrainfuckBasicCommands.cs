using System;

namespace func.brainfuck
{
    public class Symbols
    {
        private static readonly char[] symbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890"
            .ToCharArray();

        public static void ConvertToASC2(IVirtualMachine vm)
        {
            foreach (var ch in symbols)
            {
                vm.RegisterCommand(ch, vm => vm.Memory[vm.MemoryPointer] = (byte)ch);
            }
        }
    }

    public class BrainfuckBasicCommands
    {
        public static void MovepPointer(IVirtualMachine vm)
        {
            vm.RegisterCommand('<', vm =>
            {
                vm.MemoryPointer = Calculate(vm.MemoryPointer, -1, vm.Memory.Length);
            });

            vm.RegisterCommand('>', machine =>
            {
                machine.MemoryPointer = Calculate(machine.MemoryPointer, 1, machine.Memory.Length);
            });
        }

        public static void ChangeByteValue(IVirtualMachine vm)
        {
            vm.RegisterCommand('+', vm =>
            {
                var bytes = vm.Memory[vm.MemoryPointer];
                var length = vm.Memory.Length;

                vm.Memory[vm.MemoryPointer] = bytes == 255
                    ? vm.Memory[vm.MemoryPointer] = 0
                    : (byte)Calculate(bytes, 1, length);
            });

            vm.RegisterCommand('-', vm =>
            {
                var bytes = vm.Memory[vm.MemoryPointer];
                var length = vm.Memory.Length;

                vm.Memory[vm.MemoryPointer] = bytes == 0
                    ? vm.Memory[vm.MemoryPointer] = 255
                    : (byte)Calculate(bytes, -1, length);
            });
        }

        public static int Calculate(int a, int b, int modulus)
        {
            return (a + modulus + b % modulus) % modulus;
        }

        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            Symbols.ConvertToASC2(vm);

            MovepPointer(vm);

            ChangeByteValue(vm);

            vm.RegisterCommand('.', machine => write((char)machine.Memory[machine.MemoryPointer]));

            vm.RegisterCommand(',', machine => machine.Memory[machine.MemoryPointer] = (byte)read());
        }
    }
}
#region
/*
using System;


namespace func.brainfuck
{
    public static class Cosntans
    {
        static readonly char[] chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
        public static void Constants(IVirtualMachine vm)
        {
            foreach (var ch in chars)
            {
                vm.RegisterCommand(ch, v => vm.Memory[vm.MemoryPointer] = (byte)ch);
            }
        }
    }

    public class BrainfuckBasicCommands
	{
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{          
            vm.RegisterCommand('<', b => 
			{
				vm.MemoryPointer++;
				if (vm.MemoryPointer > vm.Memory.Length)
				{
					vm.MemoryPointer = 0;
				}
			});

			vm.RegisterCommand('>', b => 
			{
                vm.MemoryPointer--;
                if (vm.MemoryPointer < vm.Memory.Length)
                {
                    vm.MemoryPointer = vm.Memory.Length;
                }
            });

            vm.RegisterCommand('+', b => 
			{
                var bytes = vm.Memory[vm.MemoryPointer];
                var length = vm.Memory.Length;

                vm.Memory[vm.MemoryPointer] = bytes == 255
                ? vm.Memory[vm.MemoryPointer] = 0
                    : (byte)Calc(bytes, 1, length);

            });

            vm.RegisterCommand('-', b => 
            {

                var bytes = vm.Memory[vm.MemoryPointer];
                var length = vm.Memory.Length;

                vm.Memory[vm.MemoryPointer] = bytes == 0
                ? vm.Memory[vm.MemoryPointer] = 255
                    : (byte)Calc(bytes, 1, length);
            });

            vm.RegisterCommand(',', b => write((char)vm.Memory[vm.MemoryPointer]));

            vm.RegisterCommand('.', b => vm.Memory[vm.MemoryPointer] = (byte)read());
		}

        public static int Calc(int a, int b, int modulus)
        {
            return (a + modulus + b % modulus) % modulus;
        }
    }
}
*/
#endregion