using System;

namespace dol_cli.Utilities
{
    public interface IConsoleWrapper
    {
        void WriteLine(string value);
        void Write(string value);
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }
    }
}